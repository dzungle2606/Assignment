using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelixAssignment.BAL;
using HelixAssignment.DAL;

using HelixAssignment.ErrorHandlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HelixAssignment
{
    public class Startup
    {
        private readonly ILogger _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;

            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register the DB context to the container
            services.AddDbContext<HelixDbContext>(opt => opt.UseInMemoryDatabase("HelixDB"));

            _logger.LogInformation("Added DbContext to services");

            services.AddMvc()
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            services.AddSingleton<DbContext, HelixDbContext>();
            services.AddScoped<DbContext, HelixDbContext>();

            services.AddSingleton<IHelixEventService, HelixEventService> ();
            services.AddScoped<IHelixEventService, HelixEventService>();

            services.AddSingleton<IHelixEventDbService, HelixEventDbService>();
            services.AddScoped<IHelixEventDbService, HelixEventDbService>();

            services.AddSingleton<IProductDbService, ProductDbService>();
            services.AddScoped<IProductDbService, ProductDbService>();

            services.AddSingleton<IHelixEventProductDbService, HelixEventProductDbService>();
            services.AddScoped<IHelixEventProductDbService, HelixEventProductDbService>();


            /***********
                //// Authentication Scheme for JWT
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                {
                    options.Authority = "test.felix.assignment";
                    options.Audience = "FelixAssignment";
                    options.TokenValidationParameters.ValidateLifetime = true;
                    options.TokenValidationParameters.ClockSkew = TimeSpan.FromMinutes(60);
                });

                services.AddAuthorization(opts =>
                {
                    opts.AddPolicy("SurveyCreator", p =>
                    {
                        // Using value text for demo show, else use enum : ClaimTypes.Role
                        p.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "SurveyCreator");
                    });
                });
            ********/

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                _logger.LogInformation("In Development environment");

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<CustomExceptionMiddleware>();
            app.UseMvc();
        }
    }
}
