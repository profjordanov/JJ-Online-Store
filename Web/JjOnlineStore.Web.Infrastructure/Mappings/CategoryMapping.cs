using AutoMapper;
using JjOnlineStore.Common.ViewModels.Admin.Categories;
using JjOnlineStore.Data.Entities;

namespace JjOnlineStore.Web.Infrastructure.Mappings
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryViewModel>(MemberList.Destination);
        }
    }
}