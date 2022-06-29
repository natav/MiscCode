using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangfireWinService
{ 
    public class Startup
    {
        /// <summary>
        /// Configure Hangfire Connection string, dashboard
        /// </summary>
        /// <param name="appBuilder"></param>
        public void Configuration(IAppBuilder appBuilder)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage(ConfigurationManager
                .ConnectionStrings["HangFireConnectionString"].ConnectionString);
            appBuilder.UseHangfireDashboard();
            appBuilder.UseHangfireServer();

            var jobSvc = new HangFireService();
            jobSvc.ScheduleJobs();
        }
    }
}