using System.Collections.Generic;
using System.Runtime.Caching;

namespace CachingSolutionsSamples
{
    internal abstract class MemoryCache<TModel> : ICache<TModel>
        where TModel : class
    {
        private ObjectCache _cache = MemoryCache.Default;

        protected abstract string Prefix { get; }

        protected abstract CacheItemPolicy Policy { get; }

        public IEnumerable<TModel> Get(string forUser)
        {
            return (IEnumerable<TModel>)_cache.Get(Prefix + forUser);
        }

        public void Set(string forUser, IEnumerable<TModel> categories)
        {
            _cache.Set(Prefix + forUser, categories, Policy);
        }
    }
}
