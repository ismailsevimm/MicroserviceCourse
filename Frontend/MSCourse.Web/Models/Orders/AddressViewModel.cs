using System.ComponentModel.DataAnnotations;

namespace MSCourse.Web.Models.Orders
{
    public class AddressViewModel
    {
        [Display(Name = "Province")]
        public string Province { get; set; }

        [Display(Name = "District")]
        public string District { get; set; }

        [Display(Name = "Street")]
        public string Street { get; set; }

        [Display(Name = "ZipCode")]
        public string ZipCode { get; set; }

        [Display(Name = "Line")]
        public string Line { get; set; }
    }
}
