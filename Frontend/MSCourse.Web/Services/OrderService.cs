using MSCourse.Shared.Dtos;
using MSCourse.Shared.Services.Interfaces;
using MSCourse.Web.Models.OrderModels;
using MSCourse.Web.Models.PaymentModels;
using MSCourse.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MSCourse.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IPaymentService _paymentService;
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;
        private readonly ICatalogService _catalogService;

        public OrderService(HttpClient httpClient, IPaymentService paymentService, IBasketService basketService, ISharedIdentityService sharedIdentityService, ICatalogService catalogService)
        {
            _httpClient = httpClient;
            _paymentService = paymentService;
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
            _catalogService = catalogService;
        }

        public async Task<OrderCreatedViewModel> Create(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.Get();

            PayWithCardInput paymentInfoInput = new()
            {
                CardName = checkoutInfoInput.Card.CardName,
                CardNumber = checkoutInfoInput.Card.CardNumber,
                CVV = checkoutInfoInput.Card.CVV,
                Expiration = checkoutInfoInput.Card.Expiration,
                TotalPrice = basket.TotalPrice
            };

            var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);

            if (!responsePayment)
            {
                return new() { Error = "Payment failed.", IsSuccessful = false };
            }

            OrderCreateInput orderCreateInput = new()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AddressViewModel
                {
                    Province = checkoutInfoInput.Address.Province,
                    District = checkoutInfoInput.Address.District,
                    Street = checkoutInfoInput.Address.Street,
                    ZipCode = checkoutInfoInput.Address.ZipCode,
                    Line = checkoutInfoInput.Address.Line
                }
            };



            foreach (var item in basket.BasketItems)
            {
                var course = await _catalogService.GetByCourseIdAsync(item.CourseId);

                var orderItem = new OrderItemViewModel
                {
                    ProductId = item.CourseId,
                    ProductName = item.CourseName,
                    Price = item.GetCurrentPrice,
                    PictureUrl = course.PictureUrl
                };

                orderCreateInput.OrderItems.Add(orderItem);
            }

            var response = await _httpClient.PostAsJsonAsync<OrderCreateInput>("orders", orderCreateInput);

            if (!response.IsSuccessStatusCode)
            {
                return new() { Error = "Payment failed.", IsSuccessful = false };
            }

            var result = await response.Content.ReadFromJsonAsync<Response<OrderCreatedViewModel>>();

            result.Data.IsSuccessful = true;

            await _basketService.Delete();

            return result.Data;
        }

        public async Task<List<OrderViewModel>> GetOrder()
        {
            var response = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");

            return response.Data;
        }

        public async Task<OrderSuspendViewModel> SuspendOrder(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.Get();

            OrderCreateInput orderCreateInput = new()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AddressViewModel
                {
                    Province = checkoutInfoInput.Address.Province,
                    District = checkoutInfoInput.Address.District,
                    Street = checkoutInfoInput.Address.Street,
                    ZipCode = checkoutInfoInput.Address.ZipCode,
                    Line = checkoutInfoInput.Address.Line
                }
            };

            foreach (var item in basket.BasketItems)
            {
                var course = await _catalogService.GetByCourseIdAsync(item.CourseId);

                var orderItem = new OrderItemViewModel
                {
                    ProductId = item.CourseId,
                    ProductName = item.CourseName,
                    Price = item.GetCurrentPrice,
                    PictureUrl = course.PictureUrl
                };

                orderCreateInput.OrderItems.Add(orderItem);
            }

            PayWithCardInput paymentInfoInput = new()
            {
                CardName = checkoutInfoInput.Card.CardName,
                CardNumber = checkoutInfoInput.Card.CardNumber,
                CVV = checkoutInfoInput.Card.CVV,
                Expiration = checkoutInfoInput.Card.Expiration,
                TotalPrice = basket.TotalPrice,
                Order = orderCreateInput
            };

            var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);

            if (!responsePayment)
            {
                return new() { Error = "Payment failed.", IsSuccessful = false };
            }

            await _basketService.Delete();

            return new() { IsSuccessful = true };
        }
    }
}
