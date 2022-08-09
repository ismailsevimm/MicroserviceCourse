using MSCourse.Web.Models.DiscountModels;
using System.Threading.Tasks;

namespace MSCourse.Web.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountViewModel> Get(string discountCode);
    }
}
