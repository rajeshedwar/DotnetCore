using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DistributedCacheDemo
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDistributedMemoryCache();

            //services.AddDistributedSqlServerCache(config =>
            //{
            //    config.ConnectionString = "Data Source = 172.25.163.79;  User ID = sa1; Password = Password123; Initial Catalog = CacheDB;";
            //    config.SchemaName = "dbo";
            //    config.TableName = "CacheTable";
            //});

            //services.AddStackExchangeRedisCache(config =>
            //{
            //    config.Configuration = Configuration.GetValue<string>("Redis:host");
            //    config.InstanceName = Configuration.GetValue<string>("Redis:name");
            //});

            services.AddSession(options =>
            {
                options.Cookie.Name = ".ASPSESSIONCOOKIE";
                options.Cookie.MaxAge = TimeSpan.FromDays(5);
                options.IdleTimeout = TimeSpan.FromMinutes(15);
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.Use(async (context, next) =>
            {
                context.Items["Flag"] = $"Verified by Middleware  {DateTime.Now}";
                await next.Invoke();
            });

            app.UseStaticFiles();
           // app.UseCookiePolicy();
            app.UseSession();

            //app.UseSession(new SessionOptions {
            //    Cookie = new CookieBuilder()
            //    {
            //        Name = ".ASPSESSIONCOOKIE",
            //        MaxAge = TimeSpan.FromDays(5)
            //    },
            //    IdleTimeout=TimeSpan.FromMinutes(10)
        
            //});

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
