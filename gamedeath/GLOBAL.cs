using gamedeath.pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;

namespace gamedeath
{
    class GLOBAL
    {
        public static string fromE = "stop.go@inbox.lv";
        public static string fromPass = "D59DyDe?Kd";

        public static int CurPage; //startSign=1 newAcc=2 TRUEGAMEPAGE=3 write=4 
        public static int CurRole;

        public  static int CurUser;
               
        public static int curLVL(int xp)
        {
            int l=0;
            int k=10;
            

            while (xp>0)
            {
                if (xp>k)
                {
                    l++;
                    xp -= k;
                }
                k = k * 5;
            }

            return l;

        }
        public static int nextLVL(int l)
        {
            int k = (int)Math.Pow(5, l);            
            return k;
        }


        public static string generateCode()
        {
            Random rnd = new Random();

            string text = String.Empty;
            string ALF = "7890QWERTYUIOPASDFGHJKLZXCVBNM";
            for (int i = 0; i < 5; ++i)
                text += ALF[rnd.Next(ALF.Length)];

            return text;
        }


      public static bool isValidEmail(string email) //проверка эл.почты на корректность
        {
            string patt = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isM = Regex.Match(email, patt, RegexOptions.IgnoreCase);
            if (isM.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static void SendMail(string smtpServer, string from, string password, string mailto, string caption, string message, string attachFile = null) //отправка эл.письма
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from);
                mail.To.Add(new MailAddress(mailto));
                mail.Subject = caption;
                mail.Body = message;
                if (!string.IsNullOrEmpty(attachFile))
                    mail.Attachments.Add(new Attachment(attachFile));
                SmtpClient client = new SmtpClient();
                client.Host = smtpServer;

                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(from.Split('@')[0], password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                mail.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }


    }
}
