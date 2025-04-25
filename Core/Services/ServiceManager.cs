using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager(IBasketRepository _basketRepository, ICacheRepository _cacheRepository,UserManager<User> _userManager,IUnitOfWork _unitOfWork, IMapper _mapper) : IServiceManager
    {
        private readonly Lazy<IProductServices>_productServices = new Lazy<IProductServices>(() =>new ProductService(_unitOfWork,_mapper));
        private readonly Lazy<IBasketService> _basketServices = new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));
        private readonly Lazy<ICacheService> _cacheServices = new Lazy<ICacheService>(() => new CacheService(_cacheRepository));
        private readonly Lazy<IAuthenticationService> _authenticationServices = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager));
        public IProductServices productServices => _productServices.Value;

        public IBasketService basketServices => _basketServices.Value;

        public ICacheService cacheServices => _cacheServices.Value;

        public IAuthenticationService authenticationServices => _authenticationServices.Value;
    }
}
