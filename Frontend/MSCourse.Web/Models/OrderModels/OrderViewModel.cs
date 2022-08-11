using System;
using System.Collections.Generic;

namespace MSCourse.Web.Models.OrderModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public AddressViewModel Address { get; set; }
        public string BuyerId { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
    }
}
