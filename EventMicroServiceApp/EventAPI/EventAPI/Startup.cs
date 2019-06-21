using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventAPI.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using EventAPI.Repository;
using EventAPI.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using EventAPI.Extension;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Mvc.Formatters;
using EventAPI.CustomFormatters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EventAPI
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
            services.AddDbContext<EventDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Sqlconnection"));
            });

            services.AddScoped<IEventRepository<EventData>, EventRepository<EventData>>();
            services.AddScoped<IEventRepository<EventUser>, EventRepository<EventUser>>();
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Info
                {
                    Title = "Event Management API",
                    Version = "1.0",
                    Contact = new Contact { Name = "Rajesh", Email = "rajesh@html.com" },
                    Description = "This api gives the functions for adding"
                });
            });

            services.AddCors(options=> {

                options.AddDefaultPolicy(configure =>
                {
                    configure.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });

                options.AddPolicy("AllowAll", config =>
                {
                    config.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });


                options.AddPolicy("AllowPartners", config =>
                {
                    config.WithOrigins("*.Synergetics.com", "*.microsoft.com")
                    .WithMethods("GET")
                    .WithHeaders("Content-Type", "Authorization", "Accept");
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration.GetValue<string>("Jwt:Issuer"),
                        ValidAudience = Configuration.GetValue<string>("Jwt:Audience"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("Jwt:Secret")))
                    };
                });

            services.AddMvc(options => {
                options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
                options.OutputFormatters.Insert(0, new CsvOutputFormatter());
            })
                .AddXmlDataContractSerializerFormatters() // xmlDataContractSerializer
               // .AddXmlSerializerFormatters() //System.xml.serialization.xmlserialiser
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                // Error handling in WEB API core
                app.ConfigureExceptionHandler();
            }
            //app.UseStatusCodePages();
            //app.UseStatusCodePages("application/json", "{0}, Client side error occured");
            //app.UseStatusCodePages("application/json", "{0}, Client side error occured 
           

            app.UseCors("AllowAll");

            //app.UseCors(config => 
            //{
            //    //config.AllowAnyOrigin()
            //    //.AllowAnyMethod()
            //    //.AllowAnyHeader();

            //    config.WithOrigins("*.Synergetics.com", "*.microsoft.com")
            //    .WithMethods("GET")
            //    .WithHeaders("Content-Type", "Authorization", "Accept");

            //    config.WithOrigins("*.hexaware.com")
            //    .WithMethods("GET")
            //    .WithHeaders("Content-Type", "Authorization", "Accept");

            //});

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Event Management API");
                options.RoutePrefix = "";
            });
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
