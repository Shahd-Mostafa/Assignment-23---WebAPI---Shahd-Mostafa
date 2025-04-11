using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper) : IServiceManager
    {
        private readonly Lazy<IProductServices>_productServices = new Lazy<IProductServices>(() =>new ProductService(_unitOfWork,_mapper));
        public IProductServices productServices => _productServices.Value;
    }
}
