using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZipcodeInfo.ApiClients;
using ZipcodeInfo.DomainClasses;
using ZipcodeInfo.Processors;

namespace ZipcodeInfo
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
            services.AddTransient<IValidator<Zipcode>, ZipcodeValidator>();
            services.AddTransient<IZipcodeInfoProcessor, ZipcodeInfoProcessor>();
            services.AddTransient<IGoogleMapsApiClient, GoogleMapsApiClient>();
            services.AddTransient<IOpenWeatherApiClient, OpenWeatherApiClient>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddHttpClient("googleapis", c =>
            {
                c.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            services.AddHttpClient("openweather", c =>
            {
                c.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                    });
            });
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
