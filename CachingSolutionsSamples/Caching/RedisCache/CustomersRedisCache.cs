using System;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Caching.RedisCache
{
    internal class CustomersRedisCache : RedisCache<Customer>
    {
        public CustomersRedisCache(string hostName) : base(hostName)
        {
        }

        protected override string Prefix => "Cache_Customers";

        protected override TimeSpan Expiry => new TimeSpan(0, 1, 0);
    }
}
