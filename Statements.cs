using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLoader
{
    class Statements
    {
        public string strExDBScript { get; set; }
        public List<string> lstCEDBScript { get; set; }

        
        

        public void CreateExSysDBStmts()
        {
            List<Employee> lstXMLValues = new List<Employee>();
            DataAccess dc = new DataAccess();
            lstXMLValues = dc.GetEmpDataFromXML();
            List<Employee> lstEmpHighSal = new List<Employee>();
            lstEmpHighSal = dc.GetExSysData(lstXMLValues);
            PrepareExSysDBStmts(lstEmpHighSal);

        }

       
        public void CreateCEDBStmts()
        {
            List<Employee> lstXMLValues = new List<Employee>();
            DataAccess dc = new DataAccess();
            lstXMLValues = dc.GetEmpDataFromXML();
            List<Employee> lstEmpLowSal = new List<Employee>();
            lstEmpLowSal = dc.GetCEData(lstXMLValues);
            UpdateCEIds(lstEmpLowSal);
            PrepareCEDBStmts(lstEmpLowSal);

        }

        private void PrepareCEDBStmts(List<Employee> lstEmpLowSal)
        {
            string strCEDBScript = string.Empty;
            lstCEDBScript = new List<string>();
            foreach (var item in lstEmpLowSal)
            {
                strCEDBScript = " INSERT INTO [Employee] ([Id],[FirstName],[LastName],[Salary]) VALUES(" +
                                    "'" + item.ID + "'," +
                                    "'" + item.FirstName + "'," +
                                    "'" + item.LastName + "'," +
                                          item.Salary + ")";// +
                                                            //"\n" + "[GO]" + "\n";

                lstCEDBScript.Add(strCEDBScript);


            }
            
        }

        private void PrepareExSysDBStmts(List<Employee> lstEmpHighSal)
        {
            foreach (var item in lstEmpHighSal)
            {
                strExDBScript = strExDBScript + "INSERT INTO Employee VALUES(" +
                                    "'" + item.ID + "'," +
                                    "'" + item.FirstName + "'," +
                                    "'" + item.LastName + "'," +
                                          item.Salary + ");"; 
                      
            }
        }

        private void UpdateCEIds(List<Employee> lstEmpLowSal)
        {
            int i = 0;
            foreach (var item in lstEmpLowSal)
            {
                object obj = i+1;
                item.ID = "CE0" + obj;
                i++;
            }

        }
    }
}
