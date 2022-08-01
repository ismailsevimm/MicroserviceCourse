namespace MSCourse.Web.Models.SettingModels
{
    public class ServiceApiSettings
    {
        public string GatewayBaseUri { get; set; }
        public string IdentityBaseUri { get; set; }
        public string PhotoStockUrl { get; set; }
        public ServiceApi CatalogUrl { get; set; }
    }

    public class ServiceApi
    {
        public string Path { get; set; }
    }
}
