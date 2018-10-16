using AutoMapper;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Data;
using JjOnlineStore.Services.Data.Users;

namespace JjOnlineStore.Web.Infrastructure.Mappings
{
    public class UsersMapping : Profile
    {
        public UsersMapping()
        {
            CreateMap<ApplicationUser, UserServiceModel>(MemberList.Destination);
        }
    }
}