using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisLibrary
{
    public class RedisCacheDatabaseProvider : IRedisCacheDatabaseProvider, IDisposable
    {
        private Lazy<IConnectionMultiplexer> _connectionMultiplexer;
        private ISubscriber _subscriber;
        private Action<IConnectionMultiplexer> _reConnectionEvent;

        public RedisCacheDatabaseProvider()
        {
            _connectionMultiplexer = new Lazy<IConnectionMultiplexer>(CreateConnectionMultiplexer);
        }

        public IDatabase GetDatabase()
        {
            return _connectionMultiplexer.Value.GetDatabase(RedisConfiguration.DatabaseId);
        }

        public void RegisterReConnectionEvent(Action<IConnectionMultiplexer> actionEvent)
        {
            _reConnectionEvent = actionEvent;
        }

        private IConnectionMultiplexer CreateConnectionMultiplexer()
        {

            if (RedisConfiguration.ConnectionString.Split(':').Length > 2)
            {
                var options = new ConfigurationOptions();
                var completeConnectionString = RedisConfiguration.ConnectionString.Split(',');
                options.ServiceName = RedisConfiguration.ServiceName;
                foreach (var item in completeConnectionString)
                {
                    var endPoints = item.Split(':');
                    options.EndPoints.Add(endPoints[0], int.Parse(endPoints[1]));
                }
                options.Password = RedisConfiguration.Password;
                options.TieBreaker = "";
                options.AllowAdmin = true;
                options.SyncTimeout = RedisConfiguration.SyncTimeOut;
                options.CommandMap = CommandMap.Create(new HashSet<string> {
            // see https://redis.io/topics/sentinel
            "ping", "info", "sentinel", "subscribe", "psubscribe", "unsubscribe", "punsubscribe","auth" }, true);

                var sentinelConnection = ConnectionMultiplexer.Connect(options);

                _subscriber = sentinelConnection.GetSubscriber();
                _subscriber.Subscribe("+switch-master", (channel, message) =>
                {
                    Console.WriteLine("*******************");
                    Console.WriteLine("redis reload" + message);
                    Console.WriteLine("*******************");


                    _connectionMultiplexer = new Lazy<IConnectionMultiplexer>(() =>
                    {

                        var messageInfos = message.ToString().Split(' ');
                        var reloadIp = messageInfos[3];
                        var reloadPort = messageInfos[4];
                        var reloadOptions = new ConfigurationOptions();
                        reloadOptions.EndPoints.Add(reloadIp, int.Parse(reloadPort));
                        reloadOptions.Password = RedisConfiguration.Password;


                        Console.WriteLine($"reload redis:{reloadIp}:{reloadPort}");

                        return ConnectionMultiplexer.Connect(reloadOptions);
                    });
                    if (_reConnectionEvent != null)
                    {
                        _reConnectionEvent.Invoke(_connectionMultiplexer.Value);
                    }
                });

                _subscriber.Subscribe("+odown", (channel, message) =>
                {
                    Console.WriteLine($"+odown {message}");
                });

                _subscriber.Subscribe("+failover-detected", (channel, message) =>
                {
                    Console.WriteLine($"+failover-detected {message}");
                });

                var masterAddress = sentinelConnection.GetServer(options.EndPoints[0]).SentinelGetMasterAddressByName(options.ServiceName);
                Console.WriteLine("*******************");
                Console.WriteLine("redis connection master:" + masterAddress);
                Console.WriteLine("*******************");

                var newOptions = new ConfigurationOptions();
                newOptions.EndPoints.Add(masterAddress);
                newOptions.Password = RedisConfiguration.Password;

                return ConnectionMultiplexer.Connect(newOptions);
            }
            return ConnectionMultiplexer.Connect(ConfigurationOptions.Parse(RedisConfiguration.ConnectionString));

        }

        public void Dispose()
        {
            _connectionMultiplexer = null;
            _subscriber = null;
            _reConnectionEvent = null;
        }
    }
}
