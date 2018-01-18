using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cache.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace Cache.Controllers
{
    internal static class CacheKeys
    {
        public const string Entry = "MyEntryName";
    }

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private ICustomCache _cache;

        public ValuesController(ICustomCache cache)
        {
            _cache = cache;
        }

        [HttpGet]
        public string Get()
        {
            return GetCachedTime().ToLongTimeString();
        }

        private DateTime GetCachedTime()
        {
            if (_cache.HasValue(CacheKeys.Entry))
                return _cache.Get<DateTime>(CacheKeys.Entry);

            DateTime cacheEntry = DateTime.Now;
            _cache.Set(CacheKeys.Entry, cacheEntry);
            return cacheEntry;
        }
    }
}
