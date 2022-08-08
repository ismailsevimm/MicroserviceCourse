using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MSCourse.Web.Models.CatalogModels
{
    public class CourseCreateInput
    {
        [Display(Name="Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Price")]
        [Required]
        public decimal Price { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Picture")]
        public string Picture { get; set; }

        [Display(Name="Picture")]
        public IFormFile PhotoFormFile{ get; set; }

        [Display(Name = "User")]
        public string UserId { get; set; }

        [Display(Name = "Category")]
        [Required]
        public string CategoryId { get; set; }

        [Display(Name = "Feature")]
        public FeatureViewModel Feature { get; set; }
    }
}
