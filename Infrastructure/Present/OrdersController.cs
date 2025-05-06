using Microsoft.AspNetCore.Authorization;
using Shared.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Present
{
    [Authorize]
    public class OrdersController (IServiceManager _serviceManager) : ApiController
    {
        [HttpPost()]
        public async Task<ActionResult<OrderResultDto>> Create(OrderRequest request)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await _serviceManager.orderServices.CreateOrderAsync(request,email!));
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<OrderResultDto>> GetById(int id)
            => Ok(await _serviceManager.orderServices.GetOrderByIdAsync(id));

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResultDto>>> GetOrdersForUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await _serviceManager.orderServices.GetOrdersForUserAsync(email!));
        }

        [AllowAnonymous]
        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodResult>>> GetDeliveryMethods()
        {
            return Ok(await _serviceManager.orderServices.GetDeliveryMethodsAsync());
        }
    }
}
