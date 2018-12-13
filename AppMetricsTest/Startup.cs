using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using App.Metrics;

using App.Metrics.Extensions.Reporting.InfluxDB;

using App.Metrics.Extensions.Reporting.InfluxDB.Client;

using App.Metrics.Reporting.Interfaces;
using App.Metrics.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AppMetricsTest
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
            var database = "MyMetrics";

            var uri = new Uri("http://127.0.0.1:8086");

            var infuxUser = "admin";
            var infuxPwd = "admin";

            services.AddMetrics(options =>
            {
                options.WithGlobalTags((globalTags, info) =>
                {
                    globalTags.Add("app", info.EntryAssemblyName);
                    globalTags.Add("env", "stage");
                });
            })
           .AddHealthChecks(
                 factory => {
                     //通过HTTP访问BaiDu,看是否正常,间隔10秒
                     factory.RegisterHttpGetHealthCheck("百度是否访问正常", new Uri("https://www.baidu.com/"), TimeSpan.FromSeconds(10));
                     //检查是否能ping通百度
                     factory.RegisterPingHealthCheck("百度 ping", "baidu.com", TimeSpan.FromSeconds(10));
                     //检测占用内存是否超过2G
                     factory.RegisterProcessPhysicalMemoryHealthCheck("占用内存是否超过阀值(2G)", (2048L * 1024L) * 1024L);
                     //检测专用内存占用量是否超过阀值(2G)
                     factory.RegisterProcessPrivateMemorySizeHealthCheck("专用内存占用量是否超过阀值(2G)", (2048L * 1024L) * 1024L);
                     //检测虚拟内存占用是否超过阀值(2G)
                     factory.RegisterProcessVirtualMemorySizeHealthCheck("虚拟内存占用量是否超过阀值(2G)", (2048L * 1024L) * 1024L);
                 }
                )
           .AddReporting(
               factory =>
               {
                   factory.AddInfluxDb(
                       new InfluxDBReporterSettings
                       {
                           InfluxDbSettings = new InfluxDBSettings(database, uri),
                           ReportInterval = TimeSpan.FromSeconds(5)
                       });
               })
           .AddMetricsMiddleware(options => options.IgnoredHttpStatusCodes = new[] { 404 });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc(options => options.AddMetricsResourceFilter()).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            app.UseMetrics();
            app.UseMetricsReporting(lifetime);
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
