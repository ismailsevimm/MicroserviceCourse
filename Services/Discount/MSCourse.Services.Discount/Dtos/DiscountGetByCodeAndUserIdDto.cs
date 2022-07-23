using System.ComponentModel.DataAnnotations;

namespace MSCourse.Services.Discount.Dtos
{
    public class DiscountGetByCodeAndUserIdDto
    {
        [Required]
        public string Code { get; set; }

        public string UserId { get; set; }
    }
}
