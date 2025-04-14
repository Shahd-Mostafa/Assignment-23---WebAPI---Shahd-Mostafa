using Domain.Exceptions;
using Services.Specification;
using Shared.SpecificationParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductServices
    {
        public async Task<PaginatedResult<ProductResultDto>> GetAllProductAsync(ProductSpecificationParameters specificationParameters)
        {
            // specs
            var specs = new ProductWithBrandAndTypeSpecification(specificationParameters);
            var products = await _unitOfWork.GetRepository<Products, int>().GetAllAsync(specs);
            var productCount = await _unitOfWork.GetRepository<Products, int>().CountAsync(new ProductCountSpecification(specificationParameters));
            var productsDto = _mapper.Map<IEnumerable<ProductResultDto>>(products);
            return new PaginatedResult<ProductResultDto>
                (specificationParameters.PageIndex,specificationParameters.PageSize, productCount, productsDto
                );
        }

        public async Task<IEnumerable<BrandResultDto>> GetProductBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var brandsDto = _mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return brandsDto;

        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var specs = new ProductWithBrandAndTypeSpecification(id);
            var product = await _unitOfWork.GetRepository<Products, int>().GetAsync(specs);
            var productDto = _mapper.Map<ProductResultDto>(product);
            return productDto ?? throw new ProductNotFoundException(id);

        }

        public async Task<IEnumerable<TypeResultDto>> GetProductTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var typesDto = _mapper.Map<IEnumerable<TypeResultDto>>(types);
            return typesDto;
        }
    }
}
