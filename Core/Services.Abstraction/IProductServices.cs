using Shared.SpecificationParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IProductServices
    {
        Task<IEnumerable<ProductResultDto>> GetAllProductAsync(ProductSpecificationParameters specificationParameters);
        Task<ProductResultDto> GetProductByIdAsync(int id);
        Task<IEnumerable<TypeResultDto>> GetProductTypesAsync();
        Task<IEnumerable<BrandResultDto>> GetProductBrandsAsync();
    }
}
