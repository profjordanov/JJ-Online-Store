using AspNetCoreTemplate.Data.Models;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using JjOnlineStore.Web.Infrastructure;
using Microsoft.Extensions.Logging;

namespace JjOnlineStore.Web
{
    public class Startup
    {
	    public Startup(IHostingEnvironment env)
	    {
		    var builder = new ConfigurationBuilder()
			    .SetBasePath(env.ContentRootPath)
			    .AddJsonFile("appsettings.json" , optional: false , reloadOnChange: true)
			    .AddJsonFile($"appsettings.{env.EnvironmentName}.json" , optional: true)
			    .AddEnvironmentVariables();
		    Configuration = builder.Build();
	    }

	    public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext(Configuration.GetConnectionString("DbConnectionString"));

			services.AddIdentity();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory , JjOnlineStoreDbContext dbContext)
        {
			//TODO: ADD automapper

            if (env.IsDevelopment())
            {
	            dbContext.Database.EnsureCreated();
				app.UseDeveloperExceptionPage();
	            app.UseDatabaseErrorPage();
			}
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

	        loggerFactory.AddLogging(Configuration.GetSection("Logging"));

			app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

	        app.UseAuthentication();

			app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
