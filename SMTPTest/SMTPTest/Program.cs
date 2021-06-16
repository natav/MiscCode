using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Mail;

namespace SMTPTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("ols-noreply@legistar.com"); // ("natalya.varshavskaya@granicus.com");

                message.To.Add("natalyav@granicus.com"); // ("natalya.varshavskaya@granicus.com");

                message.Subject = "my subject - Test";
                message.Body = "mail body";
                string MailServer = "smtp.granicuslabs.com"; //"10.3.5.24"; //"smtp.dev.granicus.com"; //"smtp.granicuslabs.com"; // "localhost";

                SmtpClient emailClient = new SmtpClient(MailServer);
                emailClient.Port = 587; // 25; //587; // 25;
                emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                emailClient.EnableSsl = false;

                emailClient.Send(message);

                Console.WriteLine("SMTP Server: " + MailServer + "; Port: " + emailClient.Port+ " Sent to: " + message.To + ". Sent From: " + message.From + ". Done!");
                Console.ReadLine();
            }
           catch (Exception ex)
            {
                Console.WriteLine("An Error Occured" + ex.InnerException + " " + ex.Message);
                Console.ReadLine();
            }
        }
    }
    }
