using System.Collections.Generic;

namespace MSCourse.Web.Models.Orders
{
    public class OrderCreateInput
    {
        public OrderCreateInput()
        {
            this.OrderItems = new List<OrderItemViewModel>();
        }

        public string BuyerId { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; }

        public AddressViewModel Address { get; set; }
    }
}
