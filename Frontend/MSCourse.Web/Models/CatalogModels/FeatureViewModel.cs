using System.ComponentModel.DataAnnotations;

namespace MSCourse.Web.Models.CatalogModels
{
    public class FeatureViewModel
    {
        [Display(Name ="Course Duration")]
         
        public int Duration { get; set; }
    }
}
