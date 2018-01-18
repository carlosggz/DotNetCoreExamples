using Cache.Core;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*.............................................................................................
 
 It needs a database connection and a table with the following structure:
 
    CREATE TABLE[dbo].[CustomCache](    
        [Id] [nvarchar](449) NOT NULL,   --max len= 900, 449*2 < 900
        [Value][varbinary](max) NOT NULL,   
        [ExpiresAtTime][datetimeoffset](7) NOT NULL,   
        [SlidingExpirationInSeconds][bigint] NULL,   
        [AbsoluteExpiration][datetimeoffset](7) NULL,   
        CONSTRAINT[pk_Id] PRIMARY KEY CLUSTERED([Id] ASC) 
     )                              

 ...............................................................................................*/

namespace Cache.Infrastructure
{
    public class SqlServerCacheAdapter : ICustomCache
    {
        private readonly SqlServerCache _cache = null;
        private readonly TimeSpan _expiration;

        public SqlServerCacheAdapter(IConfiguration configuration)
        {
            _expiration = new TimeSpan(0, 0, configuration.GetValue<int>("cacheSeconds"));

            var options = new SqlServerCacheOptions()
            {
                SchemaName = configuration.GetValue<string>("cacheSchema"),
                TableName = configuration.GetValue<string>("cacheTable"),
                ConnectionString = configuration.GetValue<string>("cacheConnection"),
                DefaultSlidingExpiration = _expiration
            };

            _cache = new SqlServerCache(Options.Create<SqlServerCacheOptions>(options));
        }

        #region ICustomCache

        public bool HasValue(string cacheEntry)
        {
            return _cache.Get(cacheEntry) != null;
        }

        public T Get<T>(string cacheEntry)
        {
            var bytes = Encoding.UTF8.GetString(_cache.Get(cacheEntry));
            var deserialized = JsonConvert.DeserializeObject<T>(bytes);
            return deserialized;
        }

        public void Set<T>(string cacheEntry, T value)
        {
            var serialized = JsonConvert.SerializeObject(value);
            var bytes = Encoding.UTF8.GetBytes(serialized);
            _cache.Set(cacheEntry, bytes, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = _expiration });
        }

        public void Remove(string cacheEntry)
        {
            _cache.Remove(cacheEntry);
        }

        #endregion
    }
}
