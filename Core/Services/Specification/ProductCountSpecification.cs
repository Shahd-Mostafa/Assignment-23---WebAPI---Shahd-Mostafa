using Shared.SpecificationParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specification
{
    public class ProductCountSpecification : Specifications<Products>
    {
        public ProductCountSpecification(ProductSpecificationParameters specs) : base(
            p => (!specs.BrandId.HasValue || p.BrandId == specs.BrandId.Value) &&
            (!specs.TypeId.HasValue || p.TypeId == specs.TypeId.Value) &&
            (string.IsNullOrEmpty(specs.Search) || p.Description.ToLower().Trim().Contains(specs.Search.ToLower().Trim())) &&
            (string.IsNullOrEmpty(specs.Search) || p.Name.ToLower().Trim().Contains(specs.Search.ToLower().Trim()))
            )
        { }
    }
}
