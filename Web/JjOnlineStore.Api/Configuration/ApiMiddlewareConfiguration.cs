using Microsoft.AspNetCore.Builder;

namespace JjOnlineStore.Api.Configuration
{
    public static class ApiMiddlewareConfiguration
    {
        public static void UseSwagger(this IApplicationBuilder app, string endpointName)
        {
            app.UseSwagger();
            app.UseSwaggerUI(setup =>
            {
                setup.RoutePrefix = string.Empty;

                setup.SwaggerEndpoint(
                    url: "/swagger/v1/swagger.json",
                    name: endpointName);
            });
        }
    }
}