using AutoMapper;
using JjOnlineStore.Common.ViewModels.Products;
using JjOnlineStore.Data.Entities;

namespace JjOnlineStore.Web.Infrastructure.Mappings
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductViewModel>(MemberList.Destination)
                .ForMember(dest => dest.ShortDescription, src => src.Ignore());
        }
    }
}