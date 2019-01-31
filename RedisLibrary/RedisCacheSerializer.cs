using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisLibrary
{
    public class RedisCacheSerializer : IRedisCacheSerializer
    {
        public T Deserialize<T>(RedisValue objbyte)
        {
            string redisValueJsonString = objbyte;
            return JsonConvert.DeserializeObject<T>(redisValueJsonString);
        }

        public string Serialize(object value, Type type)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}
