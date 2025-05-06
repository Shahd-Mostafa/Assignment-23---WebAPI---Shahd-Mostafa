using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Present
{
    public class BasketController(IServiceManager _serviceManager) : ApiController
    {
        [HttpGet("{basketId}")]
        public async Task<ActionResult<BasketDto>> Get(string basketId)
        {
            var basket = await _serviceManager.basketServices.GetBasketByIdAsync(basketId);
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDto>> Update([FromBody] BasketDto basket)
        {
            var updatedBasket = await _serviceManager.basketServices.UpdateBasketAsync(basket);
            return Ok(updatedBasket);
        }
        [HttpDelete("{basketId}")]
        public async Task<IActionResult> Delete(string basketId)
        {
            var result = await _serviceManager.basketServices.DeleteBasketAsync(basketId);
            return NoContent();
        }

    }
}
