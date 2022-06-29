﻿using System;
using Microsoft.Owin.Hosting;

public class BootStrap
{
    private IDisposable _host;

    /// <summary>
    /// Configure owin hosting options for the hangfire
    /// </summary>
    public void Start()
    {
        var options = new StartOptions { Port = 8999 };
        _host = WebApp.Start<Startup>(options);
        Console.WriteLine();
        Console.WriteLine("HangFire has started");
        Console.WriteLine("Dashboard is available at http://localhost:8999/hangfire");
        Console.WriteLine();
    }

    public void Stop()
    {
        _host.Dispose();
    }
}