using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<Products, ProductResultDto>()
                .ForMember(pr=>pr.BrandName,options=>options.MapFrom(p=>p.Brand.Name))
                .ForMember(pr => pr.TypeName, options => options.MapFrom(p => p.Type.Name))
                .ForMember(pr => pr.PictureUrl, options => options.MapFrom<ProductPicUrlResolver>());
            CreateMap<ProductBrand, BrandResultDto>();
            CreateMap<ProductType, TypeResultDto>();
        }
    }
}
