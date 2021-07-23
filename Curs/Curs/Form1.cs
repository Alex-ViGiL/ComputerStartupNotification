using Microsoft.Win32;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Windows.Forms;

namespace Curs
{
    public partial class Form1 : Form
    {

        static string pathDirectory = $@"C:\Users\{SystemInformation.UserName}\ComputerStartupNotificationSavesInfo";
        string pathEmail = pathDirectory + "\\Email.txt";
        string pathInfo = pathDirectory + "\\Info.txt";
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Create directory

            if (!Directory.Exists(pathDirectory))
            {
                Directory.CreateDirectory(pathDirectory);
            }

            //Create file
            if (!File.Exists(pathDirectory + "\\Email.txt"))
            {
                File.Create(pathDirectory + "\\Email.txt");
            }

            if (!File.Exists(pathDirectory + "\\Info.txt"))
            {
                File.Create(pathDirectory + "\\Info.txt");
            }


            //Email       
            try
            {

                using (StreamReader sr = new StreamReader(pathEmail))
                {
                    textBox1.Text = sr.ReadToEnd();
                }
            }
            catch (Exception)
            {
                throw;
            }


          
            label5.Text = Convert.ToString(SystemInformation.UserName);
            label6.Text = Convert.ToString(Environment.MachineName);


            try
            {

                using (StreamReader sr = new StreamReader(pathInfo))
                {
                    string Text = "";
                    foreach (char Symbol in sr.ReadToEnd())
                    {
                        if (Symbol != '&')
                        {
                            Text += Convert.ToString(Symbol);
                        }
                        else
                        {
                            listBox1.Items.Add(Text);
                            Text = "";
                        }
                    }
                }
            }

            catch (Exception)
            {
                throw;
            }

            SendEmail();
            
        }

      

        private bool SetAutoRunValue(bool autorun, string path)
        {
            const string name = "Pc-On-Info";
            string ExePath = path;
            RegistryKey reg;

            reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try
            {
                if (autorun)
                {
                    reg.SetValue(name, ExePath);
                }
                else
                {
                    reg.DeleteValue(name);
                }

                reg.Flush();
                reg.Close();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private void SaveEmail(object sender, EventArgs e)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(pathEmail, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(textBox1.Text);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SendEmail() 
        {
            DateTime info = DateTime.Now;
            Information information = new Information(info);
            try
            {
                if (textBox1.Text != "")
                {
                    MailAddress from = new MailAddress("botmessage0@gmail.com", "Bot");

                    MailAddress to = new MailAddress(textBox1.Text);

                    MailMessage m = new MailMessage(from, to);

                    m.Subject = "Включение компютера";

                    m.Body = $"<h2> Здравствуйте {SystemInformation.UserName} ваш пк был только что включён \n {info}</h2>";

                    m.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

                    smtp.Credentials = new NetworkCredential("botmessage0@gmail.com", "smsbot12345");
                    smtp.EnableSsl = true;
                    smtp.Send(m);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Для решения данной проблемы нужно включить разрешение на сайте : https://myaccount.google.com/lesssecureapps");
                throw;
            }
            SetAutoRunValue(true, Assembly.GetExecutingAssembly().Location);

        }
    }
}
