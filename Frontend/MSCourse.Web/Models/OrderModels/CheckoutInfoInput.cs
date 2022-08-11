using System.ComponentModel.DataAnnotations;

namespace MSCourse.Web.Models.OrderModels
{
    public class CheckoutInfoInput
    {
        public AddressViewModel Address { get; set; }

        public CardViewModel Card { get; set; }

    }
}
