using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using NorthwindLibrary;

namespace CachingSolutionsSamples
{
    public class Repository<TModel>
        where TModel : class
    {
        private ICache<TModel> _cache;

        public Repository(ICache<TModel> cache)
        {
            _cache = cache;
        }

        public IEnumerable<TModel> Get()
        {
            var user = Thread.CurrentPrincipal.Identity.Name;
            var models = _cache.Get(user);

            if (models == null)
            {
                Console.WriteLine("Get from DB");

                using (var dbContext = new Northwind())
                {
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    models = dbContext.Set<TModel>().ToList();
                    _cache.Set(user, models);
                }
            }
            else
            {
                Console.WriteLine("Get from cache");
            }

            return models;
        }
    }
}
