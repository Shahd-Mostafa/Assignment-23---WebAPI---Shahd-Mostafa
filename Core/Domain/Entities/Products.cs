using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Products : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int TypeId { get; set; }
        public int BrandId { get; set; }

        public ProductType Type { get; set; }
        public ProductBrand Brand { get; set; }
    }
}
