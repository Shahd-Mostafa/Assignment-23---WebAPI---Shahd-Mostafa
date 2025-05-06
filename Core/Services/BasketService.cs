using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BasketService(IBasketRepository _basketRepository,IMapper _mapper) : IBasketService
    {
        public async Task<bool> DeleteBasketAsync(string id)
            => await _basketRepository.DeleteBasketAsync(id);

        public async Task<BasketDto?> GetBasketByIdAsync(string id)
        {
            var basket = await _basketRepository.GetBasketByIdAsync(id);
            return basket is null ? throw new BasketNotFoundException(id) : _mapper.Map<BasketDto>(basket);
        }

        public async Task<BasketDto?> UpdateBasketAsync(BasketDto basket)
        {
            var customerBasket =  _mapper.Map<CustomerBasket>(basket);
            var updatedBasket = await _basketRepository.UpdateBasketAsync(customerBasket);
            return updatedBasket is null ? throw new Exception("Can't Update Basket Now") : _mapper.Map<BasketDto>(updatedBasket);
        }
    }
}
