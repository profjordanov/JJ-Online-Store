using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace JjOnlineStore.Api.Configuration
{
    public static class ApiDependenciesConfiguration
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new Info { Title = "JjOnlineStore.Api", Version = "v1" });
            });
        }
    }
}