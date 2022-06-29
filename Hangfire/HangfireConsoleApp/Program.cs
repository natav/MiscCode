using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin.Hosting;
using System;
using Hangfire.Console;


namespace HangfireConsoleApp
{
    class Program
    {
        static void Main()
        {
            var consoleOptions = new ConsoleOptions { ExpireIn = TimeSpan.FromDays(30.0) }; // { BackgroundColor = "#000000", TextColor = "#008B8B", PollInterval = 1000, ExpireIn = TimeSpan.FromDays(30.0), TimestampColor = "#FFFFFF" };
            
            GlobalConfiguration.Configuration
                .UseSqlServerStorage("LegistarHangfireDB")
                .UseFilter(new HangfireProlongExpirationTimeAttribute())
                .UseConsole(consoleOptions);

            StartOptions options = new StartOptions();
            options.Urls.Add($"http://localhost:9095");
            options.Urls.Add($"http://127.0.0.1:9095");
            WebApp.Start<Dashboard>(options);

            //GlobalConfiguration.Configuration.UseConsole();

            using (var server = new BackgroundJobServer())
            {
                System.Console.WriteLine("Hangfire Server started...");
                System.Console.ReadKey();
            }
        }
       
    }
}
