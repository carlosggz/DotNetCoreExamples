using Cache.Core;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCache.Infrastructure
{
    /*..............................................
     
    Connect to your redis instance or use docker with a command like:

    docker run --name DemoInstance -d -p 6379:6379 redis

    ...............................................*/
    public class RedisCacheAdapter: ICustomCache
    {
        private readonly RedisCache _cache = null;
        private readonly TimeSpan _expiration;

        public RedisCacheAdapter(IConfiguration configuration)
        {
            _expiration = new TimeSpan(0, 0, configuration.GetValue<int>("cacheSeconds"));

            var options = new RedisCacheOptions()
            {
                Configuration = configuration.GetValue<string>("redisServer"),
                InstanceName = configuration.GetValue<string>("redisInstance")
            };

            _cache = new RedisCache(Options.Create<RedisCacheOptions>(options));
        }

        #region ICustomCache

        public T Get<T>(string cacheEntry)
        {
            var bytes = Encoding.UTF8.GetString(_cache.Get(cacheEntry));
            var deserialized = JsonConvert.DeserializeObject<T>(bytes);
            return deserialized;
        }

        public bool HasValue(string cacheEntry)
        {
            return _cache.Get(cacheEntry) != null;
        }

        public void Remove(string cacheEntry)
        {
            _cache.Remove(cacheEntry);
        }

        public void Set<T>(string cacheEntry, T value)
        {
            var serialized = JsonConvert.SerializeObject(value);
            var bytes = Encoding.UTF8.GetBytes(serialized);
            _cache.Set(cacheEntry, bytes, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = _expiration });
        }

        #endregion
    }
}
