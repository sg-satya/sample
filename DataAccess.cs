using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataLoader
{
    class DataAccess
    {
        List<Employee> lstXMLValues = new List<Employee>();
        XmlDocument doc = new XmlDocument();
        public DataAccess()
        {
            
            doc.Load("..\\..\\Data.xml");
        }



        public List<Employee> GetExSysData(List<Employee> lstData)
        {
              List<Employee> lstEmpHighSal = new List<Employee>();

             //Get List of employees with low salary (salary > 10000)
            lstEmpHighSal = GetHighSalData(lstData);
            return lstEmpHighSal;

        }

        public List<Employee> GetCEData(List<Employee> lstData)
        {
            List<Employee> lstEmpLowSal = new List<Employee>();

            //Get List of employees with low salary (salary <= 10000)
            lstEmpLowSal = GetLowSalData(lstData);
            return lstEmpLowSal;

        }
        public List<Employee> GetEmpDataFromXML()
        {
            List<Employee> lstXMLValues = new List<Employee>();

            XmlNodeList xnList = doc.SelectNodes("/List/Employee");

            foreach (XmlNode xn in xnList)
            {
                Employee emp = new Employee
                {
                    ID = xn["ID"].InnerText,
                    FirstName = xn["First"].InnerText,
                    LastName = xn["Last"].InnerText,
                    Salary = Convert.ToInt32(xn["Salary"].InnerText)
                };

                lstXMLValues.Add(emp);

            }
            return lstXMLValues;
        }

        public List<Employee> GetLowSalData(List<Employee> lstEmp)
        {

            List<Employee> lstLowSalEmp = lstEmp.Where(p => p.Salary<=10000).ToList();
            return lstLowSalEmp;


        }

        public List<Employee> GetHighSalData(List<Employee> lstEmp)
        {

            List<Employee> lstHighSalEmp = lstEmp.Where(p => p.Salary > 10000).ToList();
            return lstHighSalEmp;


        }

        public void UpdateCEDB()
        {
            string strError = string.Empty;
            SQLDBCE.SetConnectString("Persist Security Info = False; Data Source = '..\\..\\Employee'; ");
            //SQLDBCE.SetConnectString("Persist Security Info = False; Data Source = ' D:\\DART\\Hackathon_Artifacts\\Hacathon_NOV\\SampleApp\\DataLoader\\Employee'; ");
             
            
            Statements stmt = new Statements();

            //Prepare DBscript for Low salary data insert  
            stmt.CreateCEDBStmts();

            //Check for existance of records in the SQL CE DB
            DataTable dt = SQLDBCE.GetDataTable("SELECT * FROM Employee");

            //Delete existing records
            if (dt != null && dt.Rows.Count > 0)
                strError = SQLDBCE.ExecuteQuery("DELETE FROM Employee");

            if (string.IsNullOrEmpty(strError))
            {
                Console.WriteLine("Existing data deleted sucessfully from SQLCE DB.");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("System Encountered Error.Press Enter to Terminate.");
                Console.ReadLine();
                System.Environment.Exit(0);

            }

            //Insert low salary records into SQL CE DB

            foreach (var item in stmt.lstCEDBScript)
            {
                strError = SQLDBCE.ExecuteQuery(item);
            }


            if (string.IsNullOrEmpty(strError))
            {
                Console.WriteLine("Data inserted sucessfully into SQLCE DB.");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("System Encountered Error.Press Enter to Terminate.");
                Console.ReadLine();
                System.Environment.Exit(0);

            }
        }
    }
  }
