using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface ICacheService
    {
        Task SetCacheValue(string key, string value, TimeSpan duration);
        Task<string?> GetCachedItem(string key);
    }
}
