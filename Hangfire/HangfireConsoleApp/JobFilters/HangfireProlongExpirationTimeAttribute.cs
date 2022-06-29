using Hangfire.Common;
using Hangfire.States;
using Hangfire.Storage;
using System;

namespace HangfireConsoleApp
{
    public class HangfireProlongExpirationTimeAttribute : JobFilterAttribute, IApplyStateFilter
        {

            public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
            {
                context.JobExpirationTimeout = TimeSpan.FromDays(14);
            }

            public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
            {
                context.JobExpirationTimeout = TimeSpan.FromDays(14);

            }
        }
   
}
