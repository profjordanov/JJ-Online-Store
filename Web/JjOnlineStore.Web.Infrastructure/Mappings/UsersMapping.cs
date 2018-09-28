using AutoMapper;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Data;

namespace JjOnlineStore.Web.Infrastructure.Mappings
{
    public class UsersMapping : Profile
    {
        public UsersMapping()
        {
            CreateMap<ApplicationUser, RegisterServiceModel>(MemberList.Destination);
        }
    }
}