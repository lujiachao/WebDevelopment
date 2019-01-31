using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace RedisLibrary
{
    public class RedisCache
    {
        private IDatabase _database;
        private readonly IRedisCacheDatabaseProvider _redisCacheDatabaseProvider;
        private readonly IRedisCacheSerializer _serializer;
        private string Name { get; }

        public TimeSpan? DefaultSlidingExpireTime { get; set; }

        public RedisCache(string name, IRedisCacheDatabaseProvider redisCacheDatabaseProvider, IRedisCacheSerializer redisCacheSerializer)
        {
            _redisCacheDatabaseProvider = redisCacheDatabaseProvider;
            _database = redisCacheDatabaseProvider.GetDatabase();
            _serializer = redisCacheSerializer;
            Name = name;
            _redisCacheDatabaseProvider.RegisterReConnectionEvent((connection) => _database = connection.GetDatabase());
        }

        // ljc
        public virtual async Task<string> GetOrDefaultSingle(string key)
        {
            var redisValue = await _database.StringGetAsync(GetLocalizedKey(key));
            return redisValue;
        }

        //ljc
        public virtual async Task<T> GetOrDefaultObjectSingle<T>(string key)
        {
            var redisValue = await _database.StringGetAsync(key);
            return redisValue.HasValue ? Deserialize<T>(redisValue) : default(T);
        }

        //ljc
        public virtual async Task<T> GetOrDefault<T>(string key)
        {
            var redisValue = await _database.StringGetAsync(GetLocalizedKey(key));
            return redisValue.HasValue ? Deserialize<T>(redisValue) : default(T);
        }

        //ljc
        public virtual async Task setSingle(string key, string value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            if (value == null)
            {
                return;
            }

            await _database.StringSetAsync(GetLocalizedKey(key), value, absoluteExpireTime ?? slidingExpireTime ?? DefaultSlidingExpireTime);
        }

        public virtual async Task Set(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            if (value == null)
            {
                //需要添加日志
                //throw new TPException(EnumTPException.Redis_Error);
                return;
            }

            var type = value.GetType();
            await _database.StringSetAsync(GetLocalizedKey(key), Serialize(value, type), absoluteExpireTime ?? slidingExpireTime ?? DefaultSlidingExpireTime);
        }

        //ljc 判断redis是否存在某个key
        public virtual async Task<bool> HasKey(string key)
        {
            key = GetLocalizedKey(key);
            return await _database.KeyExistsAsync(key);
        }

        //ljc 删除redis单个键值对
        public virtual async Task<bool> DelKey(string key)
        {
            key = GetLocalizedKey(key);
            return await _database.KeyDeleteAsync(key);
        }

        public virtual async Task<long> Increment(string key)
        {
            return await _database.StringIncrementAsync(GetLocalizedKey(key));
        }

        protected virtual string Serialize(object value, Type type)
        {
            return _serializer.Serialize(value, type);
        }

        protected virtual T Deserialize<T>(RedisValue objbyte)
        {
            return _serializer.Deserialize<T>(objbyte);
        }

        protected virtual string GetLocalizedKey(string key)
        {
            return "n:" + Name + ",c:" + key;
        }
    }
}
