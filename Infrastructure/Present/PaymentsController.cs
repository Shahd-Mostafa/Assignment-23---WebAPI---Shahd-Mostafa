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
        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            await _serviceManager.paymentServices.UpdateOrderPaymentStatus(json, Request.Headers["Stripe-Signature"]);
            return Ok();
        }
    }
}
