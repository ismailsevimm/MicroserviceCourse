using MSCourse.Web.Models.OrderModels;
using System.ComponentModel.DataAnnotations;

namespace MSCourse.Web.Models.PaymentModels
{
    public class PayWithCardInput
    {
        [Display(Name = "Card Name")]
        public string CardName { get; set; }

        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        [Display(Name = "Expiration (MM/YY)")]
        public string Expiration { get; set; }

        [Display(Name = "CVV")]
        public string CVV { get; set; }

        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Order")]
        public OrderCreateInput Order { get; set; }
    }
}
