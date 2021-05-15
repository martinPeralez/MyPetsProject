using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyPets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPets
{
    public class Startup
    {
        //   F i e l d s   &   P r o p e r t i e s

        private IConfiguration _configuration { get; }

        //   C o n s t r u c t o r s
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //   M e t h o d s

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<AppDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            string dbConnectionString = System.Environment.GetEnvironmentVariable("MyPetsDbConnectionString");
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(dbConnectionString));

            services.AddScoped<IPetRepository, EfPetRepository>();
            services.AddScoped<IUserRepository, EfUserRepository>();
            services.AddScoped<IHistoryRepository, EfHistoryRepository>();
            services.AddScoped<IEmailRepository, GmailEmailRepository>();
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddMemoryCache();
            //services.AddDistributedMemoryCache();
            services.AddSession(
                 //options =>
                 //{
                 //    options.IdleTimeout = TimeSpan.FromSeconds(10);
                 //}
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
