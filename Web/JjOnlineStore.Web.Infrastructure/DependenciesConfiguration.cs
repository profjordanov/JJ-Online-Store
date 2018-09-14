using System;
using AspNetCoreTemplate.Data.Models;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
	}
}