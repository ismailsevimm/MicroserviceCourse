using System.ComponentModel.DataAnnotations;

namespace MSCourse.Web.Models.Orders
{
    public class CardViewModel
    {
        [Display(Name = "Card Name")]
        public string CardName { get; set; }

        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        [Display(Name = "Expiration (MM/YY)")]
        public string Expiration { get; set; }

        [Display(Name = "CVV")]
        public string CVV { get; set; }
    }
}
