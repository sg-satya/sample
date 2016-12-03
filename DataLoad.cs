using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLoader
{
    class DataLoad
    {
        public void LoadData()
        {
            Console.WriteLine("Start writing DDLS into Flat file for external System use....");
            Console.WriteLine();

            GenerateExtDataFile();

            Console.WriteLine("Start loading data into SQL Server CE embedded DB....");
            Console.WriteLine();

            GenerateCEData();

            Console.WriteLine("Start Updating Image");

            GenerateImageData();
        }
        public void GenerateExtDataFile()
        {
            
            string strDBScripts = string.Empty;
            Statements stmt = new Statements();
            stmt.CreateExSysDBStmts();
            strDBScripts = stmt.strExDBScript;
            Util.WriteToFile(strDBScripts);
            Console.WriteLine("Sucessfully written DDLs with data in the Flat file.");
            Console.WriteLine();
        }

        public void GenerateCEData()
        {
            DataAccess dc = new DataAccess();
            dc.UpdateCEDB();
        }

        public void GenerateImageData()
        {
            Util.WriteStringOnImage();
            Console.WriteLine("Sucessfully updated image.");
            Console.WriteLine();
        }
    }
}
