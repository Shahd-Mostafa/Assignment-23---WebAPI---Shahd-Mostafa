using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IServiceManager
    {
        IProductServices productServices { get; }
        IBasketService basketServices { get; }
        ICacheService cacheServices { get; }
        IAuthenticationService authenticationServices { get; }
        IOrderService orderServices { get; }
        IPaymentService paymentServices { get; }
    }
}
