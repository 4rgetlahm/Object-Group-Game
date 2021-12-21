using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Server.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                httpContext.Request.EnableBuffering();
            }
            catch
            {
                Logger.Log("Exception occured on middleware");
            }
            Logger.Log("Incoming " + httpContext.Request.Method + " request to: " + httpContext.Request.Path);
            string bodyContent = await new StreamReader(httpContext.Request.Body).ReadToEndAsync();
            Logger.Log("Request body: " + bodyContent);
            httpContext.Request.Body.Position = 0;

            Stream originalBody = httpContext.Response.Body;

            try
            {
                using (var memStream = new MemoryStream())
                {
                    httpContext.Response.Body = memStream;

                    await _next(httpContext);

                    memStream.Position = 0;
                    string responseBody = new StreamReader(memStream).ReadToEnd();

                    Logger.Log("Request " + httpContext.Request.Path + " fulfilled");
                    Logger.Log("Response body: " + responseBody);

                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);
                }

            }
            finally
            {
                httpContext.Response.Body = originalBody;
            }

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
