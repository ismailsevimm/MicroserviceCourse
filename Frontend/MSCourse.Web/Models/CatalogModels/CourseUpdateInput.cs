using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MSCourse.Web.Models.CatalogModels
{
    public class CourseUpdateInput
    {
         
        public string Id { get; set; }

        [Display(Name = "Name")]
         
        public string Name { get; set; }

        [Display(Name = "Price")]
         
        public decimal Price { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Picture")]
        public string Picture { get; set; }

        [Display(Name = "Picture")]
        public string PictureUrl { get; set; }

        [Display(Name = "Upload Picture")]
        public IFormFile PhotoFormFile { get; set; }

         
        public string UserId { get; set; }

        [Display(Name = "Category")]
         
        public string CategoryId { get; set; }

        [Display(Name = "Feature")]
         
        public FeatureViewModel Feature { get; set; }
    }
}
