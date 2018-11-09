using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace JjOnlineStore.Web.Infrastructure
{
	public static class MiddlewareConfiguration
	{
		public static void AddLogging(
		    this ILoggerFactory loggerFactory,
		    IConfigurationSection loggingConfiguration)
		{
		    loggerFactory.AddConsole(loggingConfiguration);
		    loggerFactory.AddFile("Logs/jj-online-store-{Date}.log");
		    loggerFactory.AddDebug();
        }

	    public static IServiceCollection AddSerilogServices(
	        this IServiceCollection services,
	        LoggerConfiguration configuration)
	    {
	        Log.Logger = configuration.CreateLogger();
	        AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();
	        return services.AddSingleton(Log.Logger);
	    }
    }
}