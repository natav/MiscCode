using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangfireWindowsService
{
    public class LegistarService
    {
        private BackgroundJobServer _server;

        public void Start()
        {
            _server = new BackgroundJobServer();
        }

        public void Stop()
        {
            _server.Dispose();
        }
    }
}