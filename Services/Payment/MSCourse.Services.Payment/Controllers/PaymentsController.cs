using MassTransit;
using Microsoft.AspNetCore.Mvc;
using MSCourse.Services.Payment.Models;
using MSCourse.Shared.ControllerBases;
using MSCourse.Shared.MessageQueries;
using System.Threading.Tasks;

namespace MSCourse.Services.Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : CustomControllerBase
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public PaymentsController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> ReceivePayment(PaymentDto paymentDto)
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new System.Uri("queue:create-order-service"));

            CreateOrderMessageCommand createOrderMessageCommand = new()
            {
                BuyerId = paymentDto.Order.BuyerId,
                Address = new Address
                {
                    Province = paymentDto.Order.Address.Province,
                    District = paymentDto.Order.Address.District,
                    Street = paymentDto.Order.Address.Street,
                    Line = paymentDto.Order.Address.Line,
                    ZipCode = paymentDto.Order.Address.ZipCode
                }
            };

            foreach (var item in paymentDto.Order.OrderItems)
            {
                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    PictureUrl = item.PictureUrl,
                    Price = item.Price
                };

                createOrderMessageCommand.OrderItems.Add(orderItem);
            }

            await sendEndpoint.Send<CreateOrderMessageCommand>(createOrderMessageCommand);

            return CreateActionResultInstance(Shared.Dtos.Response<string>.Success(200));
        }
    }
}
