using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api1.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace api1
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
            services.AddDbContext<ContosouniversityContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<AppSetting>(Configuration.GetSection("AppSettings"));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("A");
                try
                {
                    await next();

                }
                catch (System.Exception)
                {

                    throw;
                }
                await context.Response.WriteAsync("Z");
            });

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("123");
                await next();
                await context.Response.WriteAsync("456");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("WILL");
            });
        }
    }
}
