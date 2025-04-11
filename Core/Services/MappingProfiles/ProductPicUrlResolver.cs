using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class ProductPicUrlResolver :IValueResolver<Products, ProductResultDto, string>
    {
        public string Resolve(Products source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return Path.Combine("https://localhost:7019/", source.PictureUrl);
            }
            return null!;
        }
    }
}
