using Microsoft.AspNetCore.Http;
using MSCourse.Web.Models.PhotoStockModels;
using System.Threading.Tasks;

namespace MSCourse.Web.Services.Interfaces
{
    public interface IPhotoStockService
    {
        Task<PhotoViewModel> UploadPhoto(IFormFile photo);

        Task<bool> DeletePhoto(string url);
    }
}
