using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RedisLibrary;
using RedisLibraryTest.JsonHelper;

namespace RedisLibraryTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RedisConfigurationFromJson>(Configuration.GetSection("RedisConfiguration"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,IOptions<RedisConfigurationFromJson> options)
        {
            RedisConfiguration.ConnectionString = JsonConfigurationHelper.GetAppSettings(Configuration, options).ConnectionString;
            RedisConfiguration.DatabaseId = JsonConfigurationHelper.GetAppSettings(Configuration, options).DatabaseId;
            RedisConfiguration.Password = JsonConfigurationHelper.GetAppSettings(Configuration, options).Password;
            RedisConfiguration.ServiceName = JsonConfigurationHelper.GetAppSettings(Configuration, options).ServiceName;
            RedisConfiguration.SyncTimeOut = JsonConfigurationHelper.GetAppSettings(Configuration, options).SyncTimeOut;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
