using System;
using System.ComponentModel.DataAnnotations;

namespace MSCourse.Services.Discount.Dtos
{
    public class DiscountUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int Rate { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public DateTime ActivationEndTime { get; set; }
    }
}
