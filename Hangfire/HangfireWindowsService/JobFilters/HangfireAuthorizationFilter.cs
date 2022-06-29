using Microsoft.Owin;
using Hangfire.Dashboard;
[assembly: OwinStartup(typeof(HangfireWindowsService.JobFilters.HangfireAuthorizationFilter))]


namespace HangfireWindowsService.JobFilters
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        //public bool Authorize([NotNull] DashboardContext context)
        //{
        //    var httpContext = context.GetHttpContext();
        //    return httpContext.User.Identity.IsAuthenticated && httpContext.User.IsInRole("HangfireAdmin");
        //}


        public bool Authorize(DashboardContext context)
        {

            var owinContext = new OwinContext(context.GetOwinEnvironment());

            if (owinContext == null ||
                owinContext.Authentication == null ||
                owinContext.Authentication.User == null ||
                owinContext.Authentication.User.Identity == null)
            {
                //may need to add logging that this has happend.
                return false;
            }

            // Allow all authenticated users that have HangfireAdmin role
            return owinContext.Authentication.User.Identity.IsAuthenticated && owinContext.Authentication.User.IsInRole("HangfireAdmin");

            //return HttpContext.Current.User.Identity.IsAuthenticated;


            ////var owinContext = context.GetOwinEnvironment();

            ////if (owinContext.ContainsKey("server.User"))
            ////{
            ////    if (owinContext["server.User"] is ClaimsPrincipal)
            ////    {
            ////        return (owinContext["server.User"] as ClaimsPrincipal).Identity.IsAuthenticated;
            ////    }
            ////    else if (owinContext["server.User"] is GenericPrincipal)
            ////    {
            ////        return (owinContext["server.User"] as GenericPrincipal).Identity.IsAuthenticated;
            ////    }
            ////}
            ////return true;  //false;

            //var context = new OwinContext(owinEnvironment);

            //return context.Authentication.User.Identity.IsAuthenticated;
        }
    }
}
