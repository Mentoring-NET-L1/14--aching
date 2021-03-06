﻿using System;
using System.Data.SqlClient;
using System.Runtime.Caching;

using NorthwindLibrary;

namespace CachingSolutionsSamples.Caching.MemoryCache
{
    internal class CategoriesMemoryCache : MemoryCache<Category>
    {
        protected override string Prefix => "Cache_Categories";

        protected override CacheItemPolicy Policy
        {
            get
            {
                var policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(60.0);
                policy.ChangeMonitors.Add(new SqlChangeMonitor(
                    new SqlDependency(
                        new SqlCommand("SELECT * FROM [dbo].[Categories]"))));
                return policy;
            }
        }
    }
}
