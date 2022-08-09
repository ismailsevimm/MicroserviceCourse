using System.ComponentModel.DataAnnotations;

namespace MSCourse.Web.Models.DiscountModels
{
    public class DiscountApplyInput
    {
        [Display(Name = "Code")]
        public string Code { get; set; }
    }
}
