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

        public ProductWithBrandAndTypeSpecification() : base(null!)
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);
        }
    }
}
