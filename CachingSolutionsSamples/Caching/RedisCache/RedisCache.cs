using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

using StackExchange.Redis;

namespace CachingSolutionsSamples
{
    internal abstract class RedisCache<TModel> : ICache<TModel>
        where TModel : class
    {
        private ConnectionMultiplexer _redisConnection;
        private DataContractSerializer _serializer;

        protected RedisCache(string hostName)
        {
            _redisConnection = ConnectionMultiplexer.Connect(hostName);
            _serializer = new DataContractSerializer(typeof(IEnumerable<TModel>));
        }

        protected abstract string Prefix { get; }

        protected abstract TimeSpan Expiry { get; }

        public IEnumerable<TModel> Get(string forUser)
        {
            var db = _redisConnection.GetDatabase();
            byte[] cache = db.StringGet(Prefix + forUser);
            if (cache == null)
                return null;

            return (IEnumerable<TModel>)_serializer.ReadObject(new MemoryStream(cache));
        }

        public void Set(string forUser, IEnumerable<TModel> models)
        {
            var db = _redisConnection.GetDatabase();
            var key = Prefix + forUser;

            if (models == null)
            {
                db.StringSet(key, RedisValue.Null);
            }
            else
            {
                var stream = new MemoryStream();
                _serializer.WriteObject(stream, models);
                db.StringSet(key, stream.ToArray(), Expiry);
            }
        }
    }
}
