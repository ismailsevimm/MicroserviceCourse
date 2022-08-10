using System;
using System.ComponentModel.DataAnnotations;

namespace MSCourse.Web.Models.DiscountModels
{
    public class DiscountViewModel
    {
        public string UserId { get; set; }

        [Display(Name = "Rate")]
        public int Rate { get; set; }

        [Display(Name = "Code")]
        public string Code { get; set; }

        [Display(Name = "End Time")]
        public DateTime ActivationEndTime { get; set; }
    }
}
