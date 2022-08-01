using MSCourse.Web.Models;
using System.Threading.Tasks;

namespace MSCourse.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}
