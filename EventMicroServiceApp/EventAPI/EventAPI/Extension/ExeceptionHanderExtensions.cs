using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EventAPI.Extension
{
    public static class ExeceptionHanderExtensions
    {
        public static IApplicationBuilder ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseExceptionHandler(config =>
            {
                config.Run(async (context) =>
                {
                    dynamic error = null;

                    var exPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    if(exPathFeature.Error is IOException)
                    {
                        error = new { Message = "IOException raised" };
                    }
                    else if (exPathFeature.Error is InvalidOperationException)
                    {
                        error = new { Message = "InvalidOperationException raised" };
                    }
                    else
                    {
                        error = new { Message = "Some othere exception raised" };
                    }
                    context.Response.ContentType = "application/json";
                    
                    string errorMessage = JsonConvert.SerializeObject(error);
                    //using Microsoft.AspNetCore.Http;
                    await context.Response.WriteAsync(errorMessage);
                });
            });
        }
    }
}
