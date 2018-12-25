using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindLibrary;
using System.Linq;
using System.Threading;
using CachingSolutionsSamples.Caching.MemoryCache;
using CachingSolutionsSamples.Caching.RedisCache;

namespace CachingSolutionsSamples
{
	[TestClass]
	public class CacheTests
	{
		[TestMethod]
		public void MemoryCache()
		{
			var repository = new Repository<Category>(new CategoriesMemoryCache());

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(repository.Get().Count());
				Thread.Sleep(100);
			}
		}

		[TestMethod]
		public void RedisCache()
		{
			var repository = new Repository<Customer>(new CustomersRedisCache("localhost"));

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(repository.Get().Count());
				Thread.Sleep(100);
			}
		}
	}
}
