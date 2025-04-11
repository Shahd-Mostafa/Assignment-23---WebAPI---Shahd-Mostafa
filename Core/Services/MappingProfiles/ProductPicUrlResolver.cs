using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class ProductPicUrlResolver(IConfiguration config) :IValueResolver<Products, ProductResultDto, string>
    {
        public string Resolve(Products source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{config["BaseUrl"]}{source.PictureUrl}";
            }
            return null!;
        }
    }
}
