using AutoMapper;
using JjOnlineStore.Common.ViewModels.Account;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Extensions;
using JjOnlineStore.Services.Data;
using JjOnlineStore.Services.Data.Users;

namespace JjOnlineStore.Web.Infrastructure.Mappings
{
    public class UsersMapping : Profile
    {
        public UsersMapping()
        {
            CreateMap<ApplicationUser, UserServiceModel>(MemberList.Destination);
            CreateMap<RegisterViewModel, ApplicationUser>(MemberList.Source)
                .ForMember(d => d.UserName, opts => opts.MapFrom(s => s.Email))
                .ForSourceMember(s => s.Password, opts => opts.Ignore())
                .IgnoreNullSourceProperties();
        }
    }
}