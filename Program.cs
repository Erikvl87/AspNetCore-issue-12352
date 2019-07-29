using System.ServiceProcess;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;

namespace AspNetCore_12352
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = CreateWebHostBuilder(args);
			var host = builder.Build();
			var logger = host.Services.GetRequiredService<ILogger<Program>>();

			//host.RunAsService();
			// Not using RunAsService() but doing exactly the same, for debugging purposes.
			// https://github.com/aspnet/AspNetCore/blob/v2.2.5/src/Hosting/WindowsServices/src/WebHostWindowsServiceExtensions.cs#L36
			var webHostService = new WebHostService(host);

			var autoLog = webHostService.AutoLog;
			logger.LogInformation($"Value of webHostService.AutoLog: {autoLog}");

			var serviceName = webHostService.ServiceName;

			// issue: Uncomment the following line to fix the service started logs!
			// webHostService.ServiceName = "Application";

			logger.LogInformation($"Value of webHostService.ServiceName: {serviceName}");

			ServiceBase.Run(webHostService);
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.ConfigureLogging((hostingContext, logging) =>
				{
					logging.AddEventLog();
					logging.AddFilter<EventLogLoggerProvider>(typeof(Program).FullName, LogLevel.Information);
				})
				.UseStartup<Startup>();
	}
}
