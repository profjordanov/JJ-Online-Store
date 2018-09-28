using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Core;
using JjOnlineStore.Services.Data;
using Microsoft.AspNetCore.Identity;
using Optional;

namespace JjOnlineStore.Services.Business
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UsersService(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<Option<RegisterServiceModel, Error>> Register(RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var creationResult = await _userManager.CreateAsync(user, model.Password);

            if (creationResult.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Mapper.Map<RegisterServiceModel>(user).Some<RegisterServiceModel, Error>();
            }

            var creationResultErrors = string.Join(";", creationResult.Errors.Select(e => e.Description));
            return Option.None<RegisterServiceModel, Error>(new Error(creationResultErrors));
        }
    }
}