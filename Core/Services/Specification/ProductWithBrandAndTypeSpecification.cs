using Shared.SpecificationParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specification
{
    public class ProductWithBrandAndTypeSpecification : Specifications<Products>
    {
        public ProductWithBrandAndTypeSpecification(int id) : base(p=>p.Id==id)
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);
        }

        public ProductWithBrandAndTypeSpecification(ProductSpecificationParameters specs) : base(
            p=>(!specs.BrandId.HasValue || p.BrandId== specs.BrandId.Value)&&
            (!specs.TypeId.HasValue || p.TypeId == specs.TypeId.Value)&&
            (string.IsNullOrEmpty(specs.Search) || p.Description.ToLower().Trim().Contains(specs.Search.ToLower().Trim()))&&
            (string.IsNullOrEmpty(specs.Search) || p.Name.ToLower().Trim().Contains(specs.Search.ToLower().Trim()))
            )
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);

            if(specs.Sort !=null)
            {
                switch (specs.Sort)
                {
                    case ProductSort.PriceAsc:
                        AddOrderBy(p => p.Price);
                        break;
                    case ProductSort.PriceDesc:
                        AddOrderByDescending(p => p.Price);
                        break;
                    case ProductSort.NameAsc:
                        AddOrderBy(p => p.Name);
                        break;
                    case ProductSort.NameDesc:
                        AddOrderByDescending(p => p.Name);
                        break;
                    default:
                        break;
                }
            }
            AddPagination(specs.PageSize * (specs.PageIndex - 1), specs.PageSize);
        }
    }
}
