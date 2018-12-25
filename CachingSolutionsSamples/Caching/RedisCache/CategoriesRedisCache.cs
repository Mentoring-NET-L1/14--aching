using System;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Caching.RedisCache
{
    internal class CategoriesRedisCache : RedisCache<Category>
    {
        public CategoriesRedisCache(string hostName) :base(hostName)
        {
        }

        protected override string Prefix => "Cache_Categories";

        protected override TimeSpan Expiry => new TimeSpan(0, 1, 0);
    }
}
