using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Present
{
    public class PaymentsController(IServiceManager _serviceManager) : ApiController
    {
        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var paymentIntent = await _serviceManager.paymentServices.CreateOrUpdatePaymentIntentAsync(basketId);
            return Ok(paymentIntent);
        }
    }
}
