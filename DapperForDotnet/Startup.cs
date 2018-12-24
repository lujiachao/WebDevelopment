using DapperForDotnet.Common.Connection;
using DapperForDotnet.Common.Connection.ConnectionModel;
using DapperForDotnet.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace DapperForDotnet
{
    public class Startup
    {
        public static string baseDirectory = AppContext.BaseDirectory;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            services.AddTransient<IConnectionFactory,ConnectionFactory>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole(minLevel: LogLevel.Information);  //在控制台输出日志
                app.UseRequestIP(); //使用中间件
                app.UseDeveloperExceptionPage();
            }
            else
            {
                loggerFactory.AddConsole(minLevel: LogLevel.Information);
                app.UseRequestIP(); //使用中间件
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
