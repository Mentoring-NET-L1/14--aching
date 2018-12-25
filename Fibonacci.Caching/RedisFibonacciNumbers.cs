using System;
using System.Collections.Generic;

using StackExchange.Redis;

namespace Fibonacci.Caching
{
    public class RedisFibonacciNumbers : IFibonacciNumbers
    {
        private const string _numbersKey = "numbers";

        private ConnectionMultiplexer _redis = ConnectionMultiplexer.Connect("localhost");

        public RedisFibonacciNumbers()
        {
            _redis = ConnectionMultiplexer.Connect("localhost");
            var db = _redis.GetDatabase();
            if (!db.KeyExists(_numbersKey))
                db.ListRightPush(_numbersKey, new[] { (RedisValue)0, (RedisValue)1 });
        }

        public int Get(int n)
        {
            if (n < 0)
                throw new ArgumentOutOfRangeException(nameof(n), n, "Parameter must be greater than 0.");

            ExpandNumbers(n);
            return (int)_redis.GetDatabase().ListGetByIndex(_numbersKey, n);
        }

        public IEnumerable<int> GetSequence(int n)
        {
            if (n < 0)
                throw new ArgumentOutOfRangeException(nameof(n), n, "Parameter must be greater than 0.");


            ExpandNumbers(n);
            var db = _redis.GetDatabase();
            for (int i = 0; i < db.ListLength(_numbersKey); i++)
            {
                yield return (int)db.ListGetByIndex(_numbersKey, i);
            }
        }

        private void ExpandNumbers(int n)
        {
            var db = _redis.GetDatabase();
            if (db.ListLength(_numbersKey) <= n)
            {
                for (var i = db.ListLength(_numbersKey); i <= n; i++)
                {
                    int newNumber = (int)db.ListGetByIndex(_numbersKey, i - 2) + (int)db.ListGetByIndex(_numbersKey, i - 1);
                    db.ListRightPush(_numbersKey, newNumber);
                }
            }
        }
    }
}
