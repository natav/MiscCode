using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SMTPTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string smtpServer = "";
                string smtpPort = "25";
                string toAddress = "";

                for (int i = 0; i < args.Length; i++)
                {
                    string[] param = args[i].Split(':');

                    switch (param[0])
                    {
                        case "smtpServer":
                            smtpServer = param[1].ToLower();
                            break;
                        case "smtpPort":
                            smtpPort = param[1].ToLower();
                            break;
                        case "toAddress":
                            toAddress = param[1];
                            break;
                        default:
                            break;
                    }
                }

                if (args.Length != 3)
                {
                    Console.WriteLine("Please provide arguments.");
                    Console.WriteLine("SMTPTest.exe smtpServer:\"SMTP Server Address\" smtpPort:\"SMTP Server Port\" toAddress:\"Address to whom send test email\"");
                    Console.WriteLine("Press ENTER to continue...");
                    Console.ReadLine();
                    return;
                }

                MailMessage message = new MailMessage();
                message.From = new MailAddress(toAddress);

                message.To.Add(toAddress);

                message.Subject = "SMTP Test";
                message.Body = "eMail Body";
                string MailServer = smtpServer;

                SmtpClient emailClient = new SmtpClient(MailServer);
                emailClient.Port = int.Parse(smtpPort);
                emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                emailClient.EnableSsl = false;

                emailClient.Send(message);

                Console.WriteLine($"SMTP Server: {smtpServer}{Environment.NewLine}Port: {smtpPort}{Environment.NewLine}Sent To: {toAddress}{Environment.NewLine}Sent From: {toAddress}{Environment.NewLine}Sent!");
                Console.ReadLine();
            }
           catch (Exception ex)
            {
                Console.WriteLine($"An Error Occured:{Environment.NewLine}{ex.InnerException}{Environment.NewLine}{Environment.NewLine}Error Message:{Environment.NewLine}{ex.Message}");
                Console.ReadLine();
            }
        }
    }
    }
