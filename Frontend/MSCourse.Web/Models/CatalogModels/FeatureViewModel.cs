using System.ComponentModel.DataAnnotations;

namespace MSCourse.Web.Models.CatalogModels
{
    public class FeatureViewModel
    {
        [Display(Name ="Course Duration")]
        [Required]
        public int Duration { get; set; }
    }
}
