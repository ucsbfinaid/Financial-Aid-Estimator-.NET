using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ucsb.Sa.FinAid.AidEstimation;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation;
using Ucsb.Sa.FinAid.AidEstimation.Utility;

namespace UCD.AidEstimator
{
    public class Startup
    {
        private IWebHostEnvironment _environment;
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _environment = environment;
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(_configuration.GetSection("AppSettings"));

            AppSettings appSettings = _configuration.GetSection("AppSettings").Get<AppSettings>();
            services.AddSingleton<EfcCalculator>(GetEfcCalculator(appSettings.EfcCalculationConstants));
            services.AddSingleton<CostOfAttendanceEstimator>(GetCostOfAttendanceEstimator(appSettings.AidEstimationConstants));

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private EfcCalculator GetEfcCalculator(string relativeConstantsPath)
        {
            EfcCalculatorFactory factory = new EfcCalculatorFactory(MapRelativePath(relativeConstantsPath));
            return factory.GetEfcCalculator();
        }

        private CostOfAttendanceEstimator GetCostOfAttendanceEstimator(string relativeConstantsPath)
        {
            CostOfAttendanceEstimatorFactory factory = new CostOfAttendanceEstimatorFactory(MapRelativePath(relativeConstantsPath));
            return factory.GetCostOfAttendanceEstimator();
        }

        private string MapRelativePath(string relativePath)
        {
            return relativePath.Replace("~", _environment.ContentRootPath);
        }
    }
}