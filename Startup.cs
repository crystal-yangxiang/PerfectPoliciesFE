using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PerfectPoliciesFE.Models;
using PerfectPoliciesFE.Models.ViewModels;
using PerfectPoliciesFE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPoliciesFE
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
            services.AddControllersWithViews();

            services.AddDistributedMemoryCache();

            services.AddSession(c =>
            {
                c.Cookie.HttpOnly = true;
                c.Cookie.IsEssential = true;
            });

            //Services for adding Http connections
            services.AddScoped<IApiRequest<Quiz>, ApiRequest<Quiz>>();
            services.AddScoped<IApiRequest<Question>, ApiRequest<Question>>();
            services.AddScoped<IApiRequest<Option>, ApiRequest<Option>>();
            services.AddScoped<IApiRequest<QuestionsPerQuizViewModel>, ApiRequest<QuestionsPerQuizViewModel>>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddSingleton<IApiRequest<Quiz>, InMemoryRequest<Quiz>>();

            services.AddHttpClient("ApiClient", c =>
            {
                c.BaseAddress = new Uri(Configuration["WebApiUrl"]);
                // Dictates to the API which specific data to return to the front end. If no json, API will send nothing.
                c.DefaultRequestHeaders.Clear();
                c.DefaultRequestHeaders.Accept
                    .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            });
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

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
