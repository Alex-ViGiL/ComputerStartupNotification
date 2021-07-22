using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Curs
{
    class Information
    {
       public Information(DateTime info) 
        {
            string pathDirectory = @"C:\Users\aleks\ComputerStartupNotificationSavesInfo";
            string writePath = pathDirectory + "\\Info.txt";
            string DateTime = Convert.ToString(info) + "&";

            try
            {
                File.AppendAllText(writePath,DateTime, Encoding.UTF8);
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
