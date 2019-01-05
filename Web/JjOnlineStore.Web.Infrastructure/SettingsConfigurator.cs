using JjOnlineStore.Common.AppSettings.Sections;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JjOnlineStore.Web.Infrastructure
{
    public static class SettingsConfigurator
    {
        public static void AddSettings(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.Configure<AppMailSettings>(configuration.GetSection(nameof(AppMailSettings)));
        }
    }
}