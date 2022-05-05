using AspNet.Tutorial.Core.DataTransfers.Products;
using AspNet.Tutorial.Core.Entities;

using AutoMapper;

namespace AspNet.Tutorial.Core.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();

            CreateMap<ProductCreationDto, Product>();

            CreateMap<ProductModificationDto, Product>();

            CreateMap<Product, Product>()
                .ForMember(e => e.Id, option => option.Ignore())
                .ForMember(e => e.CreatedBy, option => option.Ignore())
                .ForMember(e => e.ModifiedBy, option => option.Ignore())
                .ForMember(e => e.IsDeleted, option => option.Ignore())
                .ForMember(e => e.CreatedAt, option => option.Ignore())
                .ForMember(e => e.ModifiedAt, option => option.Ignore());
        }
    }
}
