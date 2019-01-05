using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Linq;
using System.Threading.Tasks;

using static JjOnlineStore.Common.GlobalConstants;

namespace JjOnlineStore.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<JjOnlineStoreDbContext>().Database.Migrate();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<ApplicationRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

                Task.Run(async () =>
                {
                    // Seed Roles
                    var roles = new[]
                    {
                        AdministratorRoleName,
                        BlogAuthorRoleName
                    };

                    foreach (var role in roles)
                    {
                        var roleExists = await roleManager.RoleExistsAsync(role);

                        if (roleExists) continue;

                        var result = await roleManager.CreateAsync(new ApplicationRole(role));
                        if (!result.Succeeded)
                        {
                            throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                        }
                    }

                    // Seed Admin User
                    var adminUser = await userManager.FindByEmailAsync(AdminEmail);
                    if (adminUser == null)
                    {
                        // Create Admin User
                        adminUser = new ApplicationUser
                        {
                            UserName = AdminUsername,
                            Email = AdminEmail,
                            IsDeleted = false,
                            PhoneNumber = "359878000000",
                            CreatedOn = DateTime.Now
                        };

                        var result = await userManager.CreateAsync(adminUser, AdminPassword);

                        // Add User to Role
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(adminUser, AdministratorRoleName);
                        }
                        else
                        {
                            throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                        }
                    }
                })
                    .GetAwaiter()
                    .GetResult();
            }
        }
    }
}