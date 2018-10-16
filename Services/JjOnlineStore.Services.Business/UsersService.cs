using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Common.ViewModels.Account;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Extensions;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core;
using JjOnlineStore.Services.Data;
using JjOnlineStore.Services.Data.Users;
using Microsoft.AspNetCore.Identity;
using Optional;

namespace JjOnlineStore.Services.Business
{
    public class UsersService : BaseService, IUsersService
    {
        public UsersService(
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

        public Task<Option<UserServiceModel, Error>> LoginAsync(CredentialsModel model) =>
            GetUser(u => u.Email == model.Email)
                .FilterAsync(user => UserManager.CheckPasswordAsync(user, model.Password), "Invalid credentials.".ToError())
                .MapAsync(async user =>
                {
                    var result = Mapper.Map<UserServiceModel>(user);
                    await SignInManager.SignInAsync(user, isPersistent: false);
                    return result;
                });

        public async Task<Option<UserServiceModel, Error>> Register(RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var creationResult = await UserManager.CreateAsync(user, model.Password);

            if (!creationResult.Succeeded)
            {
                return Option.None<UserServiceModel, Error>(
                    new Error(creationResult.Errors.Select(e => e.Description)));
            }

            await SignInManager.SignInAsync(user, isPersistent: false);
            return Mapper.Map<UserServiceModel>(user).Some<UserServiceModel, Error>();
        }
    }
}