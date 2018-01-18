using Cache.Core;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cache.Infrastructure
{
    public class InMemoryCacheAdapter: ICustomCache
    {
        private readonly IMemoryCache _cache = null;
        private readonly TimeSpan _expiration;

        public InMemoryCacheAdapter(IConfiguration configuration)
        {
            _expiration = new TimeSpan(0, 0, configuration.GetValue<int>("cacheSeconds"));

            var options = new MemoryCacheOptions()
            {
                ExpirationScanFrequency = _expiration
            };

            _cache = new MemoryCache(Options.Create<MemoryCacheOptions>(options));
        }

        #region ICustomCache

        public bool HasValue(string cacheEntry)
        {
            return _cache.Get(cacheEntry) != null;
        }

        public T Get<T>(string cacheEntry)
        {
            return _cache.Get<T>(cacheEntry);
        }

        public void Set<T>(string cacheEntry, T value)
        {
            _cache.Set(cacheEntry, value, _expiration);
        }

        public void Remove(string cacheEntry)
        {
            _cache.Remove(cacheEntry);
        }

        #endregion
    }
}
