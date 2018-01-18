using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cache.Core
{
    public interface ICustomCache
    {
        bool HasValue(string cacheEntry);
        T Get<T>(string cacheEntry);
        void Set<T>(string cacheEntry, T value);
        void Remove(string cacheEntry);
    }
}
