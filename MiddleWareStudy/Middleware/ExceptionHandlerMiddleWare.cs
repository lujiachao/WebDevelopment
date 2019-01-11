using Microsoft.AspNetCore.Http;
using MiddleWareStudy.Result;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace MiddleWareStudy.Middleware
{
    public class ExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate next;

        /// <summary>
        /// 重构方法
        /// </summary>
        /// <param name="_next"></param>
        public ExceptionHandlerMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception == null) return;
            await WriteExceptionAsync(context, exception).ConfigureAwait(false);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception exception)
        {
            //返回友好的提示
            var response = context.Response;
            response.ContentType = "application/json";

            if (exception is PrivilegeException)
            {
                await response.WriteAsync(JsonConvert.SerializeObject(new ZeusResultData()
                {
                    Code = ((PrivilegeException)exception).Code,
                    Message = exception.Message,
                })).ConfigureAwait(false);
            }
            else if (exception is Exception)
            {
                await response.WriteAsync(JsonConvert.SerializeObject(new ZeusResultData()
                {
                    Code = 400,
                    Message = exception.Message,
                })).ConfigureAwait(false);
            }
        }
    }
}
