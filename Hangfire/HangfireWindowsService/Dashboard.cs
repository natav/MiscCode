using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using HangfireWindowsService;
using HangfireWindowsService.JobFilters;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Serilog;
using System;

[assembly: OwinStartup(typeof(HangfireWindowsService.Dashboard))]

namespace HangfireWindowsService
{
    public static class MyAuthentication
    {
        public const String ApplicationCookie = "MyProjectAuthenticationType";
    }

    public class Dashboard
    {
        public void Configuration(IAppBuilder app)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            //app.UseHangfireServer();
            //var options = new DashboardOptions { AppPath = "https://www.granicus.com", Authorization =  }; // 'Back to site' link URL

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = MyAuthentication.ApplicationCookie,
                LoginPath = new PathString("/Login"),
                Provider = new CookieAuthenticationProvider(),
                CookieName = "MyCookieName",
                ExpireTimeSpan = TimeSpan.FromHours(12),
            });


            var options = new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() },
                AppPath = "https://www.granicus.com" // 'Back to site' link URL
            };
 
            app.UseHangfireDashboard("/hangfire", options);

            var jobManager = new RecurringJobManager();
            //jobManager.AddOrUpdate("job-id", Job.FromExpression(() => Method()), "*/2 * * * *");
            //jobManager.AddOrUpdate("powerful_job", () => Console.WriteLine("Powerful!"), "*/1 * * * *");
            jobManager.AddOrUpdate("password_expiration_check_job", () => TaskMethod(null), "*/1 * * * *"); // every minute
        }

        [HangfireProlongExpirationTimeAttribute]
        public void TaskMethod(PerformContext context)
        {
            Log.Information("Password expiration check Started");
            
            context.WriteLine("This is built-in console");
            context.WriteLine("Task being executed...");
            
            // create progress bar
            var progress = context.WriteProgressBar();

            progress.SetValue(10);
            
            context.WriteLine("------------------------");

            progress.SetValue(55);

            context.WriteLine("Set progress to 100%");
            progress.SetValue(100);

            Log.Information("Password expiration check Finished");
        }
    }
}
