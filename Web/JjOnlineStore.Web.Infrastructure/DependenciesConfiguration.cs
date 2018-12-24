using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Business;
using JjOnlineStore.Services.Business.Admin;
using JjOnlineStore.Services.Core;
using JjOnlineStore.Services.Core.Admin;
using JjOnlineStore.Services.Business.Storage;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System;

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
	        services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IShoppingCartService, ShoppingCartService>();
            services.AddTransient<ICartItemsService, CartItemsService>();
	        services.AddTransient<IOrdersService, OrdersService>();
	        services.AddTransient<IOrderItemsService, OrderItemsService>();
	        services.AddTransient<IPdfGenerator, PdfGenerator>();
	        services.AddTransient<IBillingService, BillingService>();
	        services.AddTransient<IUserViewedItemsService, UserViewedItemsService>();
	        services.AddTransient<IManageService, ManageService>();
	        services.AddTransient<IImageStorageService, ImageStorageService>();
	        services.AddTransient<IFileService, FileService>();
	        services.AddTransient<IProductImagesService, ProductImagesService>();
	    }
    }
}