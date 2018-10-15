using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Business;
using JjOnlineStore.Services.Business.Admin;
using JjOnlineStore.Services.Core;
using JjOnlineStore.Services.Core.Admin;

namespace JjOnlineStore.Web.Infrastructure
{
	public static class DependenciesConfiguration
	{
		public static void AddDbContext(this IServiceCollection services , string connectionString)
		{
			if(string.IsNullOrEmpty(connectionString))
			{
				throw new ArgumentException(nameof(connectionString));
			}

		    services
		        .AddDbContext<JjOnlineStoreDbContext>(opts => opts.UseSqlServer(connectionString))
		        .AddEntityFrameworkSqlServer();
        }

		public static void AddIdentity(this IServiceCollection services)
		{
			services.AddIdentity<ApplicationUser , ApplicationRole>(
					options =>
					{
						options.Password.RequireDigit = false;
						options.Password.RequireLowercase = false;
						options.Password.RequireUppercase = false;
						options.Password.RequireNonAlphanumeric = false;
						options.Password.RequiredLength = 6;
					})
				.AddEntityFrameworkStores<JjOnlineStoreDbContext>()
				.AddUserStore<ApplicationUserStore>()
				.AddRoleStore<ApplicationRoleStore>()
				.AddDefaultTokenProviders();
		}

        public static void AddIdentityStores(this IServiceCollection services)
		{
			services.AddTransient<IUserStore<ApplicationUser> , ApplicationUserStore>();
			services.AddTransient<IRoleStore<ApplicationRole> , ApplicationRoleStore>();
		}

	    public static void AddApplicationServices(this IServiceCollection services)
	    {
	        services.AddTransient<IUsersService, UsersService>();
	        services.AddTransient<IAdminCategoryService, AdminCategoryService>();
	        services.AddTransient<IAdminProductsService, AdminProductsService>();
	    }
    }
}