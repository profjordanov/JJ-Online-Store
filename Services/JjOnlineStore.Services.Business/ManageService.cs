using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Common.ViewModels.Manage;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core;
using JjOnlineStore.Services.Data.Users;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Optional;

using AutoMapper;

using System.Linq;
using System.Threading.Tasks;

using static Optional.Option;

namespace JjOnlineStore.Services.Business
{
    public class ManageService : BaseService, IManageService
    {
        public ManageService(
            JjOnlineStoreDbContext dbContext, 
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            IMapper mapper) 
            : base(dbContext)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            Mapper = mapper;
        }

        protected UserManager<ApplicationUser> UserManager;
        protected SignInManager<ApplicationUser> SignInManager;
        protected IMapper Mapper;

        public async Task<Option<UserServiceModel, Error>> ChangePasswordAsync(ChangePasswordVm model)
        {
            var user = await DbContext
                .Users
                .Where(u => u.Id == model.UserId)
                .FirstOrDefaultAsync();

            var result = await UserManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return None<UserServiceModel, Error>(new Error(result.Errors.Select(e => e.Description)));
            }

            await SignInManager.SignInAsync(user, isPersistent: false);

            return new UserServiceModel
            {
                Id = user.Id,
                Email = user.Email
            }.Some<UserServiceModel, Error>();
        }

    }
}