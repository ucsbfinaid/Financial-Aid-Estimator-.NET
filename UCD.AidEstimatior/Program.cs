///This file was copied from the Intranet application
using System;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;
using Microsoft.AspNetCore;
using NLog.LayoutRenderers;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {           
            LayoutRenderer.Register("basdir-drive-letter", (logEvent) => System.Reflection.Assembly.GetEntryAssembly().Location.Substring(0,1));

			// NLog: setup the logger first to catch all errors
			var logger = NLog.LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();
			try
			{
				logger.Debug("init main");
				BuildWebHost(args).Run();
			}
			catch (Exception ex)
			{
				//NLog: catch setup errors
				logger.Error(ex, "Stopped program because of exception");
				throw;
			}
			finally
			{
				// Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
				NLog.LogManager.Shutdown();
			}
		}
		public static IWebHost BuildWebHost(string[] args) =>
		WebHost.CreateDefaultBuilder(args)
			.UseStartup<Startup>()
			.UseApplicationInsights()
			.UseNLog()
			.Build();
	}
}
