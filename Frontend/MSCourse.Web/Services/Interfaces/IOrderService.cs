using MSCourse.Web.Models.OrderModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSCourse.Web.Services.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// Senkron bağlantı yapacak 
        /// </summary>
        /// <param name="checkoutInfoInput"></param>
        /// <returns></returns>
        Task<OrderCreatedViewModel> Create(CheckoutInfoInput checkoutInfoInput);

        /// <summary>
        /// Asenkron bağlantı yapacak RabbitMQ'ya gönderecek
        /// </summary>
        /// <param name="checkoutInfoInput"></param>
        /// <returns></returns>
        Task SuspendOrder(CheckoutInfoInput checkoutInfoInput);

        Task<List<OrderViewModel>> GetOrder();


    }
}
