using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyDapper.Connection;
using PrivilegeManagement.MiddleWare;
using PrivilegeManagement.MiddleWare.Logger;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace PrivilegeManagement
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
            #region Swagger
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v0.1.0",
                    Title = "PrivilegeManagement API",
                    Description = "权限管理系统框架说明文档",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "PrivilegeManagement.Core", Email = "PrivilegeManagement.Core@xxx.com", Url = "https://www.baidu.com" }
                });
                //就是这里
                var xmlPath = Path.Combine(basePath, "PrivilegeManagement.xml");  //这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath, true); //默认第二个参数是false,这个是controller的注释，记得修改
            });
            #endregion
            services.AddTransient<IConnectionFactory, ConnectionFactory>();            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                #region Swagger
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                });
                #endregion
                app.UseMiddleware(typeof(RequestLoggingMiddleware));
                app.UseMiddleware(typeof(ExceptionHandlerMiddleWare));

            }
            else
            {
                app.UseHsts();
                #region Swagger
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                });
                #endregion
                app.UseMiddleware(typeof(RequestLoggingMiddleware));
                app.UseMiddleware(typeof(ExceptionHandlerMiddleWare));
            }
            app.UseMvc();
        }
    }
}
