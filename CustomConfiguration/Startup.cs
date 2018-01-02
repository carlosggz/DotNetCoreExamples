using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomConfiguration.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CustomConfiguration
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ApplicationValues.Initialize(configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages(async (StatusCodeContext context) => 
            {
                var request = context.HttpContext.Request;

                var toReturn = new
                {
                    Code = context.HttpContext.Response.StatusCode,
                    Url = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}",
                    request.Method
                };

                context.HttpContext.Response.ContentType = "application/json";
                await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(toReturn));

                logger
                    .CreateLogger<Startup>()
                    .LogError($"{toReturn.Code} on url {toReturn.Url} with nethod {toReturn.Method}");
            });

            //app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
