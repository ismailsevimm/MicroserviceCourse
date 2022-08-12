using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MSCourse.Services.Basket.Consumers;
using MSCourse.Services.Basket.Services;
using MSCourse.Services.Basket.Services.Interfaces;
using MSCourse.Services.Basket.Settings;
using MSCourse.Shared.Services;
using MSCourse.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace MSCourse.Services.Basket
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
            services.AddMassTransit(x => {

                x.AddConsumer<CourseNameChangedEventConsumer>();

                //Default Port : 5672;
                x.UsingRabbitMq((context, config) =>
                {
                    config.Host(Configuration["RabbitMQUrl"], "/", host => {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    config.ReceiveEndpoint("course-name-changed-event-basket-service", e => {
                        e.ConfigureConsumer<CourseNameChangedEventConsumer>(context);
                    });

                });
            });
            services.AddMassTransitHostedService();

            var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.Authority = Configuration["IdentityServerURL"];
                opt.Audience = "resource_basket";
                opt.RequireHttpsMetadata = false;
            });

            services.AddHttpContextAccessor();
            services.AddScoped<ISharedIdentityService, SharedIdentityService>();

            services.Configure<RedisSettings>(Configuration.GetSection("RedisSettings"));

            services.AddSingleton<RedisService>(
                sp =>
                {
                    var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;

                    var redisService = new RedisService(redisSettings.Host, redisSettings.Port);

                    redisService.Connect();

                    return redisService;
                });

            services.AddScoped<IBasketService, BasketService>();

            services.AddControllers(
                opt =>
                {
                    opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MSCourse.Services.Basket", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MSCourse.Services.Basket v1"));
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
