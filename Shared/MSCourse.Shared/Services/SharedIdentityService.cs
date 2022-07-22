using Microsoft.AspNetCore.Http;
using MSCourse.Shared.Services.Interfaces;

namespace MSCourse.Shared.Services
{
    /// <summary>
    /// Bu service solution içerisindeki referans verilen tüm projelerde userId'e erişebilmek için kullanılır.
    /// </summary>
    public class SharedIdentityService : ISharedIdentityService
    {
        /// <summary>
        /// Tüm projelerde Http Context nesnesine erişebilmek için IHttpContextAccessor interface'ini kullanıyoruz.
        /// Bu interface'i kullanabilmek için çağırmak istediğimiz projenin Startup sınıfındaki ConfigureService metoduna
        /// services.AddHttpContextAccessor(); olarak eklenir.
        /// </summary>
        private IHttpContextAccessor _httpContextAccessor;

        public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst("sub").Value;
    }
}
