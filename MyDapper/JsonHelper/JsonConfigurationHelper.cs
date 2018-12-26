using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.IO;

namespace MyDapper.JsonHelper
{
    public static class JsonConfigurationHelper
    {
        public static string baseDirectory = Directory.GetCurrentDirectory();
        public static T GetAppSettings<T>(string key) where T : class, new()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(baseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var appconfig = new ServiceCollection()
                .AddOptions()
                .Configure<T>(config.GetSection(key))
                .BuildServiceProvider()
                .GetService<IOptions<T>>()
                .Value;
            return appconfig;
        }

        public static string GetAppSettingSingle(string key)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(baseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            string appconfig = config[key];
            return appconfig;

        }

        public static T GetAppSettings<T>(IConfiguration config, IOptions<T> options) where T : class, new()
        {
            var appconfig = options.Value;
            return appconfig;
        }

        public static string GetAppSettingSingle(string key, IConfiguration config)
        {
            string appconfig = config[key];
            return appconfig;
        }
    }
}
