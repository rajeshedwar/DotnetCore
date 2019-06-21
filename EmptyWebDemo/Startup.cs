using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Microsoft.Extensions.FileProviders;


namespace EmptyWebDemo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            DefaultFilesOptions options= new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("test.html");
            app.UseDefaultFiles(options);
            // app.UseDefaultFiles();


            app.UseStaticFiles();
            // app.UseStaticFiles(new StaticFileOptions(){
            //     FileProvider= new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"staticfiles")),
            //     RequestPath="/files"
            // });

            app.UseFileServer(new FileServerOptions(){
                FileProvider= new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"staticfiles")),
                RequestPath="/files",
                EnableDirectoryBrowsing=true
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions(){
                FileProvider= new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"WWWroot","images")),
                RequestPath="/images" 
            });

            // //First Middleware
            // app.Use(async(context,next)=>{
            //     context.Response.ContentType="text/html";
            //     await context.Response.WriteAsync("From First middleware.....<br>");// Execute during request
            //     await next.Invoke();
            //     await context.Response.WriteAsync("From First middleware.....<br>");// Execute during response
            // });

            // //Second Middleware
            // app.Use(async(context,next)=>{
            //     await context.Response.WriteAsync("From Second middleware.....<br>");// Execute during request
            //     await next.Invoke();
            //     await context.Response.WriteAsync("From Second middleware.....<br>");// Execute during response
            // });

            // //Middleware for /about route

            // app.Map(new PathString("/about"), (appBuilder)=>{
            //     appBuilder.Use(async(context,next)=>{
            //     await context.Response.WriteAsync("From Third middleware.....<br>");// Execute during request
            //     await next.Invoke();
            //     await context.Response.WriteAsync("From Third middleware.....<br>");// Execute during response
            //     });
            //     appBuilder.Run(async (context) =>
            //     {
            //         await context.Response.WriteAsync("About Page!<br>");
            //     });
            // });


            // app.MapWhen((context)=>context.Request.Query.ContainsKey("id"),(appBuilder)=>{
            //     appBuilder.Use(async(context,next)=>{
            //     await context.Response.WriteAsync("From Query middleware.....<br>");// Execute during request
            //     await next.Invoke();
            //     await context.Response.WriteAsync("From Query middleware.....<br>");// Execute during response
            //     });
            //     appBuilder.Run(async (context) =>
            //     {
            //         await context.Response.WriteAsync("Page with id!<br>");
            //     });
            // });

           // app.UseMiddleware<MyCustomMiddleware>();
           app.UseMyCustomMiddleware();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
