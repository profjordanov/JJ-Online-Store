using AutoMapper;

using JjOnlineStore.Api.Configuration;
using JjOnlineStore.Api.Filters;
using JjOnlineStore.Web.Infrastructure;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JjOnlineStore.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
                {
                    options.Filters.Add<ApiExceptionFilter>();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext(Configuration.GetConnectionString("DefaultConnection"));

            services.AddAutoMapper();

            services.AddSwagger();

            services.AddIdentity();
            services.AddIdentityStores();

            services.AddApplicationServices();

            services.AddRouting(routing => routing.LowercaseUrls = true);
        }

        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseSwagger("SoftUnikum API.");
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
