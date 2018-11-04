using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Common.ViewModels.Account;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core;
using JjOnlineStore.Services.Data.Users;
using JjOnlineStore.Extensions;

using Microsoft.AspNetCore.Identity;

using Optional;

using AutoMapper;

using System.Linq;
using System.Threading.Tasks;



namespace JjOnlineStore.Services.Business
{
    public class UsersService : BaseService, IUsersService
    {
        private readonly IShoppingCartService _shoppingCartService;

        public UsersService(
            JjOnlineStoreDbContext dbContext,
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            IMapper mapper, 
            IShoppingCartService shoppingCartService) 
            : base(dbContext)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            Mapper = mapper;
            _shoppingCartService = shoppingCartService;
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

        public async Task<Option<UserServiceModel, Error>> RegisterAsync(RegisterViewModel model)
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

            await _shoppingCartService.CreateForUserByItsId(user.Id);

            await SignInManager.SignInAsync(user, isPersistent: false);
            return Mapper.Map<UserServiceModel>(user)
                .Some<UserServiceModel, Error>();
        }

        public async Task SignOutAsync() =>
            await SignInManager.SignOutAsync();
    }
}