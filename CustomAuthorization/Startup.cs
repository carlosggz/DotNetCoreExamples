using CustomAuthorization.Core;
using CustomAuthorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace CustomAuthorization
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
            services
                .AddAuthentication(Constants.AuthenticationScheme)
                .AddCookie(Constants.AuthenticationScheme, options =>
                {
                    options.LoginPath = "/Login";
                    options.Cookie.Name = Constants.CookieName;
                });

            services.AddAntiforgery(options => options.HeaderName = Constants.XrsfToken);

            services.AddMvc();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.PolicySyfy, policy => policy.Requirements.Add(new ChannelRequirement(MoviesChannels.Syfy)));
                options.AddPolicy(Constants.PolicyTnt, policy => policy.Requirements.Add(new ChannelRequirement(MoviesChannels.Tnt)));

                options.AddPolicy(Constants.ClaimSyfy, policy => policy.RequireClaim(Constants.MoviesClaim, MoviesChannels.Syfy.ToString()));
                options.AddPolicy(Constants.ClaimTnt, policy => policy.RequireClaim(Constants.MoviesClaim, MoviesChannels.Tnt.ToString()));
            });

           services.AddSingleton<IAuthorizationHandler, ChannelAudienceHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "login",
                    template: "login",
                    defaults: new { controller = "Account", action = "Login" }
                );

                routes.MapRoute(
                    name: "logoff",
                    template: "logoff",
                    defaults: new { controller = "Account", action = "Logoff" }
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
            });
         }
    }
}
