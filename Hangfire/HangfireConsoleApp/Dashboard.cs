using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using HangfireConsoleApp;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartup(typeof(Dashboard))]

namespace HangfireConsoleApp
{
    public class Dashboard
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseHangfireServer();
            var options = new DashboardOptions { AppPath = "https://www.granicus.com" }; // 'Back to site' link URL
            app.UseHangfireDashboard("/hangfire", options);

            var jobManager = new RecurringJobManager();
            //jobManager.AddOrUpdate("job-id", Job.FromExpression(() => Method()), "*/2 * * * *");
            //jobManager.AddOrUpdate("powerful_job", () => Console.WriteLine("Powerful!"), "*/1 * * * *");
            jobManager.AddOrUpdate("password_expiration_check_job", () => TaskMethod(null), "*/1 * * * *"); // every minute
        }

        [HangfireProlongExpirationTimeAttribute]
        public void TaskMethod(PerformContext context)
        {
            context.WriteLine("This is built-in console");
            context.WriteLine("Task being executed...");
            context.WriteLine("------------------------");

            // create progress bar
            var progress = context.WriteProgressBar();

            progress.SetValue(10);
            context.WriteLine("Set progress to 75%"); // update value in progress bar
            progress.SetValue(75);
        }

    }
}
