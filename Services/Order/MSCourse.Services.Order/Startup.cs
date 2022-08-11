using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MSCourse.Services.Order.Application.Consumers;
using MSCourse.Services.Order.Infrastructure;
using MSCourse.Shared.Services;
using MSCourse.Shared.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace MSCourse.Services.Order
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

                x.AddConsumer<CreateOrderMessageCommandConsumer>();

                //Default Port : 5672;
                x.UsingRabbitMq((context, config) =>
                {
                    config.Host(Configuration["RabbitMQUrl"], "/", host => {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    config.ReceiveEndpoint("create-order-service", e => {
                        e.ConfigureConsumer<CreateOrderMessageCommandConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();

            var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.Authority = Configuration["IdentityServerURL"];
                opt.Audience = "resource_order";
                opt.RequireHttpsMetadata = false;
            });


            services.AddDbContext<OrderDbContext>(
                opt =>
                {
                    opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                        configure =>
                        {
                            configure.MigrationsAssembly("MSCourse.Services.Order.Infrastructure");
                        });
                });

            services.AddHttpContextAccessor();
            services.AddScoped<ISharedIdentityService, SharedIdentityService>();

            services.AddMediatR(typeof(Application.Handlers.CreateOrderCommandHandler).Assembly);

            services.AddControllers(
                opt =>
                {
                    opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));

                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MSCourse.Services.Order", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MSCourse.Services.Order v1"));
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
