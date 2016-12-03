using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataLoader
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Starting data load....");
            Console.WriteLine();

            DataLoad dl = new DataLoad();
            dl.LoadData();

            Console.WriteLine("Press Enter to close");
            Console.ReadLine();

        }
    }
}
