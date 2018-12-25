using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace Fibonacci.Caching
{
    public class FibonacciNumbers : IFibonacciNumbers
    {
        private const string _numbersKey = "numbers";

        private readonly ObjectCache _cache;

        public FibonacciNumbers()
        {
            _cache = MemoryCache.Default;
            _cache.Add(_numbersKey, new List<int>() { 0, 1 }, null);
        }

        public int Get(int n)
        {
            if (n < 0)
                throw new ArgumentOutOfRangeException(nameof(n), n, "Parameter must be greater than 0.");

            return GetNumbers(n)[n];
        }

        public IEnumerable<int> GetSequence(int n)
        {
            if (n < 0)
                throw new ArgumentOutOfRangeException(nameof(n), n, "Parameter must be greater than 0.");

            return GetNumbers(n).Take(n);
        }

        private List<int> GetNumbers(int n)
        {
            var numbers = (List<int>)_cache.Get(_numbersKey);
            if (numbers.Count <= n)
            {
                for (var i = numbers.Count; i <= n; i++)
                {
                    numbers.Add(numbers[i - 2] + numbers[i - 1]);
                }
            }
            return numbers;
        }
    }
}
