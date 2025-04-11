using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.SpecificationParameters
{
    public class ProductSpecificationParameters
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSort? Sort { get; set; }
        public string? Search { get; set; }
    }
    public enum ProductSort
    {
        NameAsc=1,
        NameDesc,
        PriceAsc,
        PriceDesc
    }
}
