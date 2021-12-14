using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Server.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RequestBodyReadingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestBodyReadingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            try
            {
                httpContext.Request.EnableBuffering();
            }
            catch
            {
                Console.WriteLine("Exception occured on middleware");
            }
            Console.WriteLine("Incoming " + httpContext.Request.Method + " request to: " + httpContext.Request.Path);
            //_logger.Log("Incoming " + httpContext.Request.Method + " request to: " + httpContext.Request.Path);
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RequestBodyReadingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestBodyReadingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestBodyReadingMiddleware>();
        }
    }
}
