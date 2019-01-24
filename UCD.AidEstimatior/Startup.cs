/* Taken from the intranet application then modified:
 * 
 * Removed references to authentication and authorization.
 * Added AppSettings
 *  
 */

using Joonasw.AspNetCore.SecurityHeaders;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Web
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // defualt all JsonConvert operations to camel case since that is the default for MVC API's
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            // Required to use the Options<T> pattern
            services.AddOptions();
            
            // Add memory cache for caching.
            services.AddMemoryCache();
            // Add in the Antiforgery service and set it up to accept a header called X-CSRF-TOKEN
            services.AddAntiforgery(options => options.HeaderName = "X-CSRF-TOKEN");
            // Add Data Protection services (i.e. encryption)
            services.AddDataProtection();
            // Enable MVC
            services.AddMvc(
                options =>
                {
                    options.Filters.Add(new Web.Models.ApiExceptionFilter());
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    options.Filters.Add(new RequireHttpsAttribute());
                }
            );

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.Configure<RazorViewEngineOptions>(options =>
            {
                // {2} is area, {1} is controller, {0} is the action    
                options.AreaViewLocationFormats.Add("~/Areas/{2}/Views/{0}" + RazorViewEngine.ViewExtension);
            });
                        
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            // Add services necessary for nonces in CSP, 32-byte nonces
            services.AddCsp(nonceByteAmount: 32);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IAntiforgery antiforgery)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/error");

                // Enable Strict Transport Security with a 30-day caching period
                app.UseHsts(new HstsOptions(TimeSpan.FromDays(30)));
            }
            
            // Content Security Policy
            app.UseCsp(csp =>
            {
                // Allow JavaScript from:
                csp.AllowScripts
                    .FromSelf() // This domain
                    .From("ajax.aspnetcdn.com")
                    .AddNonce();

                // Contained iframes can be sourced from:
                csp.AllowFrames
                    .FromNowhere(); // Nowhere, no iframes allowed

                // Allow fonts to be downloaded from:
                csp.AllowFonts
                    .FromSelf()
                    .From("ajax.aspnetcdn.com");

                // Allow other sites to put this in an iframe?
                csp.AllowFraming
                    .FromNowhere(); // Block framing on other sites, equivalent to X-Frame-Options: DENY

            });


            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areaRoute",
                  template: "{area:exists}/{controller=Home}/{action=Index}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });

        }
    }
}
