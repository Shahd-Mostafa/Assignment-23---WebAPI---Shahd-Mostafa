using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connectionMultiplexer) : IBasketRepository
    {
        private readonly IDatabase _database;
        public Task<bool> DeleteBasketAsync(string id)
            => _database.KeyDeleteAsync(id);

        public async Task<CustomerBasket?> GetBasketByIdAsync(string id)
        {
            var value = await _database.StringGetAsync(id);
            if (value.IsNullOrEmpty) return null;

            return JsonSerializer.Deserialize<CustomerBasket>(value);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket,TimeSpan? timeToLive= null)
        {
            var jsonBasket = JsonSerializer.Serialize(basket);
            var isCreatedOrUpdated = await _database.StringSetAsync(basket.Id, jsonBasket, timeToLive?? TimeSpan.FromDays(30));
            return isCreatedOrUpdated ? await GetBasketByIdAsync(basket.Id) : null;
        }
    }
}
