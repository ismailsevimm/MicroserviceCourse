using IdentityModel.Client;
using MSCourse.Shared.Dtos;
using MSCourse.Web.Models;
using System.Threading.Tasks;

namespace MSCourse.Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SigninInput signinInput);
        Task<TokenResponse> GetAccessTokenByRefreshToken();

        Task RevokeRefreshToken();
    }
}
