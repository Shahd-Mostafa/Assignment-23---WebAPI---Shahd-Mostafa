using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CacheService(ICacheRepository _cacheRepository) : ICacheService
    {
        public Task<string?> GetCachedItem(string key)
            => _cacheRepository.GetAsync(key);

        public async Task SetCacheValue(string key, object value, TimeSpan duration)
            => await _cacheRepository.SetAsync(key, value, duration);
    }
}
