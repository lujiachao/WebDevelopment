using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiddleWareStudy.DIContainer;
using MiddleWareStudy.LoggersMiddleware;
using MiddleWareStudy.Middleware;
using System;

namespace MiddleWareStudy
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
            services.AddSingleton(Configuration);
            services.AddTransient<IPlayGame, NBPlayGame>();
            services.AddScoped<DoSomething>();
            services.AddTransient<IDemoService, DemoService1>();
            services.AddTransient<IDemoService, DemoSwevice2>();
            services.UseRequestIP();
            services.AddTransient<RequestLogMiddleware>();
            //services.AddTransient<TestMiddlewareNext>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,DoSomething thing)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                //app.UseRequestIP(); //使用中间件
                //Console.WriteLine(thing.GetMessage());
                //app.UseMiddleware<TestMiddleware>();
                //app.UseMiddleware<DemoMiddleware>();
                //app.UseMiddleware<RequestLogMiddleware>();
                app.UseMiddleware<RequestLoggingMiddleware>();
                app.UseMiddleware<ExceptionHandlerMiddleWare>();
                //app.UseMiddleware<TestMiddlewareNext>();
            }
            else
            {
                app.UseHsts();
                app.UseRequestIP(); //使用中间件
            }
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
