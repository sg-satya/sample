using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataLoader
{
    static class Util
    {
        public static void GetParamsFrmXML(XmlDocument doc)
        {
             
            XmlNodeList elemList = doc.GetElementsByTagName("First");
            for (int i = 0; i < elemList.Count; i++)
            {
                Console.WriteLine("First Name:" + elemList[i].InnerXml);
            }
            Console.ReadLine();
        }

        public static void WriteToFile(string strTxt)
        {
            try
            {
                StreamWriter sw = new StreamWriter("..\\..\\DataFile.txt");
                sw.WriteLine(strTxt);
            }
            catch(Exception ex)
            {
                Console.WriteLine("System Encountered Error.Press Enter to Terminate.");
                Console.ReadLine();
                System.Environment.Exit(0);
            }

        }

        public static void WriteStringOnImage()
        {
            try
            {

                byte[] imgData = File.ReadAllBytes("..\\..\\img7.jpg");
                using (System.Drawing.Image img = System.Drawing.Image.FromStream(new MemoryStream(imgData)))
                {
                    for (int i = 1; i <= 200; i++)
                    {
                        Bitmap img1 = new Bitmap(new Bitmap(img));
                        RectangleF rectf = new RectangleF(800, 550, 200, 200);
                        Graphics g = Graphics.FromImage(img1);
                        g.DrawString(i.ToString("0000"), new Font("Thaoma", 30), Brushes.Black, rectf);
                        //img1.Save(@"D:\Img\" + i.ToString("0000") + ".jpg", ImageFormat.Jpeg);
                        img1.Save("..\\..\\CEDataSnapshot.jpg");
                        g.Flush();
                        g.Dispose();
                        img1.Dispose();
                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
