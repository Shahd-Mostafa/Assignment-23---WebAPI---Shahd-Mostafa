using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager(IBasketRepository _basketRepository,IUnitOfWork _unitOfWork, IMapper _mapper) : IServiceManager
    {
        private readonly Lazy<IProductServices>_productServices = new Lazy<IProductServices>(() =>new ProductService(_unitOfWork,_mapper));
        private readonly Lazy<IBasketService> _basketServices = new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));
        public IProductServices productServices => _productServices.Value;

        public IBasketService basketServices => _basketServices.Value;
    }
}
