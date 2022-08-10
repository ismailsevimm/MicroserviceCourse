using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSCourse.Web.Handlers;
using MSCourse.Web.Models.SettingModels;
using MSCourse.Web.Services;
using MSCourse.Web.Services.Interfaces;
using System;

namespace MSCourse.Web.Extensions
{
    public static class ServiceExtension
    {
        public static void AddHttpClientService(this IServiceCollection services, IConfiguration Configuration)
        {
            var serviceApiSettings = Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();

            services.AddHttpClient<IIdentityService, IdentityService>();

            services.AddHttpClient<ICatalogService, CatalogService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.GatewayBaseUri + serviceApiSettings.Catalog.Path);
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IPhotoStockService, PhotoStockService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.GatewayBaseUri + serviceApiSettings.PhotoStock.Path);
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IUserService, UserService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUri);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IBasketService, BasketService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.GatewayBaseUri + serviceApiSettings.Basket.Path);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IDiscountService, DiscountService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.GatewayBaseUri + serviceApiSettings.Discount.Path);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IPaymentService, PaymentService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.GatewayBaseUri + serviceApiSettings.Payment.Path);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
            
            services.AddHttpClient<IOrderService, OrderService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.GatewayBaseUri + serviceApiSettings.Order.Path);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
        }
    }
}
