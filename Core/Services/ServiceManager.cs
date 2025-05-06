using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager(IBasketRepository _basketRepository, ICacheRepository _cacheRepository,UserManager<User> _userManager,IOptions<JwtOptions> _options,IUnitOfWork _unitOfWork, IMapper _mapper,IConfiguration _configuration) : IServiceManager
    {
        private readonly Lazy<IProductServices>_productServices = new Lazy<IProductServices>(() =>new ProductService(_unitOfWork,_mapper));
        private readonly Lazy<IBasketService> _basketServices = new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));
        private readonly Lazy<ICacheService> _cacheServices = new Lazy<ICacheService>(() => new CacheService(_cacheRepository));
        private readonly Lazy<IAuthenticationService> _authenticationServices = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager,_options,_mapper));
        private readonly Lazy<IOrderService> _orderServices = new Lazy<IOrderService>(() => new OrderService(_unitOfWork, _mapper, _basketRepository));
        private readonly Lazy<IPaymentService> _paymentServices = new Lazy<IPaymentService>(() => new PaymentService(_basketRepository, _unitOfWork, _mapper, _configuration));
        public IProductServices productServices => _productServices.Value;

        public IBasketService basketServices => _basketServices.Value;

        public ICacheService cacheServices => _cacheServices.Value;

        public IAuthenticationService authenticationServices => _authenticationServices.Value;

        public IOrderService orderServices => _orderServices.Value;

        public IPaymentService paymentServices => _paymentServices.Value;
    }
}
