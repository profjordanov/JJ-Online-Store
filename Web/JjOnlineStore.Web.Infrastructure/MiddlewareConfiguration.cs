
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace JjOnlineStore.Web.Infrastructure
{
	public static class MiddlewareConfiguration
	{
		public static void AddLogging(this ILoggerFactory loggerFactory , IConfigurationSection loggingConfiguration)
		{
		}
	}
}