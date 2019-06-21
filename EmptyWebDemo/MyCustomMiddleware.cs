using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EmptyWebDemo
{

    public class MyCustomMiddleware
    {
        
    
        RequestDelegate _next;

        public MyCustomMiddleware(RequestDelegate next)
        {
            this._next=next;
        }


        public async Task Invoke(HttpContext context)
        {
            await  context.Response.WriteAsync("this is from custom middleware (Req).......<br>");
            await _next.Invoke(context);
            await  context.Response.WriteAsync("this is from custom middleware (Res).......<br>");
        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MyCustomMiddleware>();
        }
    }
}