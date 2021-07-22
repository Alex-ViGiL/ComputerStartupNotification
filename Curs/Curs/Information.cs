using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Curs
{
    class Information
    {
        public Information(DateTime info)
        {

            string pathDirectory = $@"C:\Users\{SystemInformation.UserName}\ComputerStartupNotificationSavesInfo";
            string writePath = pathDirectory + "\\Info.txt";
            string DateTime = Convert.ToString(info) + "&";

            try
            {
                File.AppendAllText(writePath, DateTime, Encoding.UTF8);
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
