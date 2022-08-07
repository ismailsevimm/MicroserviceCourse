using System.Threading.Tasks;

namespace MSCourse.Web.Services.Interfaces
{
    public interface IClientCredentialTokenService
    {
        Task<string> GetToken();
    }
}
