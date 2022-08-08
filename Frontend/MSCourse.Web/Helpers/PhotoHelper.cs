using Microsoft.Extensions.Options;
using MSCourse.Web.Models.SettingModels;

namespace MSCourse.Web.Helpers
{
    public class PhotoHelper
    {
        private readonly ServiceApiSettings _serviceApiSettings;

        public PhotoHelper(IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _serviceApiSettings = serviceApiSettings.Value;
        }

        public string GetPhotoStockUrl(string filename)
        {
            return $"{_serviceApiSettings.PhotoStockUri}/photos/{filename}";
        }
    }
}
