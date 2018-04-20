﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using GummyBearKingdom.Models;


namespace GummyBearKingdom
{

    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }
        public static string ConnectionString;
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json"); //this line replaces .AddEnvironmentVariables();
            Configuration = builder.Build();
            ConnectionString = Configuration["ConnectionStrings:DefaultConnection"];
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFrameworkMySql()
            .AddDbContext<StoreDbContext>(options =>
            options
            .UseMySql(Configuration["ConnectionStrings:DefaultConnection"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //app.UseStaticFiles();
            app.UseDeveloperExceptionPage();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
    name: "default",
    template: "{controller=Home}/{action=Index}/{id?}");
            });
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {

            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Something went wrong!");
            });
        }
    }
}