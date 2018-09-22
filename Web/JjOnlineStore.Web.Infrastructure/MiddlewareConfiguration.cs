using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace JjOnlineStore.Web.Infrastructure
{
	public static class MiddlewareConfiguration
	{
		public static void AddLogging(this ILoggerFactory loggerFactory , IConfigurationSection loggingConfiguration)
		{
		    loggerFactory.AddConsole(loggingConfiguration);
		    loggerFactory.AddFile("logs/jj-online-store-{Date}.log");
		    loggerFactory.AddDebug();
        }
	}
}