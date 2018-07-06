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
                message.From = new MailAddress("natalya.varshavskaya@granicus.com");

                message.To.Add("natalya.varshavskaya@granicus.com");

                message.Subject = "my subject - Test";
                message.Body = "mail body";
                string MailServer = "localhost";

                SmtpClient emailClient = new SmtpClient(MailServer);
                emailClient.Port = 25;
                emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                emailClient.EnableSsl = false;

                emailClient.Send(message);

                Console.WriteLine("Sent to: " + message.To + ". Sent From: " + message.From + ". Done!");
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
