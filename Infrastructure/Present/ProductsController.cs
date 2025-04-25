using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Present.Attributes;
using Shared.ErrorModels;
using Shared.SpecificationParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Present
{
    public class ProductsController(IServiceManager _serviceManager) :ApiController
    {
        [HttpGet]
        [RedisCacheAttributes(40)]
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProduct([FromQuery]ProductSpecificationParameters specificationParameters) => Ok(await _serviceManager.productServices.GetAllProductAsync(specificationParameters));

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductResultDto>> GetProductById(int id) => Ok(await _serviceManager.productServices.GetProductByIdAsync(id));

        [HttpGet("brand")]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetProductBrand()=> Ok(await _serviceManager.productServices.GetProductBrandsAsync());

        [HttpGet("type")]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetProductType() => Ok(await _serviceManager.productServices.GetProductTypesAsync());
    }
}
