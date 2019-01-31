using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisLibrary
{
    public interface IRedisCacheSerializer
    {
        T Deserialize<T>(RedisValue objbyte);

        string Serialize(object value, Type type);
    }
}
