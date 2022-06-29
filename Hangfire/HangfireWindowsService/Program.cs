using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin.Hosting;
using System;
using Hangfire.Console;
using Topshelf;
using HangfireWindowsService;
using Serilog;

namespace HangfireindowsService
{
    class Program
    {
        static void Main()
        {
            var consoleOptions = new ConsoleOptions { ExpireIn = TimeSpan.FromDays(14.0) }; // { BackgroundColor = "#000000", TextColor = "#008B8B", PollInterval = 1000, ExpireIn = TimeSpan.FromDays(30.0), TimestampColor = "#FFFFFF" };

            GlobalConfiguration.Configuration
                .UseSqlServerStorage("LegistarHangfireDB")
                .UseFilter(new HangfireProlongExpirationTimeAttribute())
                .UseConsole(consoleOptions);

            StartOptions options = new StartOptions();
            options.Urls.Add($"http://localhost:9095");
            options.Urls.Add($"http://127.0.0.1:9095");
            WebApp.Start<Dashboard>(options);

            //Topshelf
            HostFactory.Run(x =>
            {
                x.Service<LegistarService>(s =>
                {
                    s.ConstructUsing(name => new LegistarService());
                    s.WhenStarted(ls => ls.Start());
                    s.WhenStopped(ls => ls.Stop());
                });

                x.StartAutomatically();
                x.EnableServiceRecovery(r => r.RestartService(1));
                x.RunAsLocalSystem();

                x.SetDescription("Legistar Windows Service");
                x.SetDisplayName("LegistarWindowsService");
                x.SetServiceName("LegistarWindowsService");

                x.UseSerilog(CreateLogger());
            });
        }

        private static ILogger CreateLogger()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File($"c:\\logs\\LegistarWindowsService_.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            return logger;
        }
    }
}
