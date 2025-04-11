using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductServices
    {
        public async Task<IEnumerable<ProductResultDto>> GetAllProductAsync()
        {
            var products = await _unitOfWork.GetRepository<Products, int>().GetAllAsync();
            var productsDto = _mapper.Map<IEnumerable<ProductResultDto>>(products);
            return productsDto;
        }

        public async Task<IEnumerable<BrandResultDto>> GetProductBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var brandsDto = _mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return brandsDto;

        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.GetRepository<Products, int>().GetAsync(id);
            var productDto = _mapper.Map<ProductResultDto>(product);
            return productDto;

        }

        public async Task<IEnumerable<TypeResultDto>> GetProductTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var typesDto = _mapper.Map<IEnumerable<TypeResultDto>>(types);
            return typesDto;
        }
    }
}
