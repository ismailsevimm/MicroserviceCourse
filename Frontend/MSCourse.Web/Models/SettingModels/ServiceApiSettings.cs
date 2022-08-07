namespace MSCourse.Web.Models.SettingModels
{
    public class ServiceApiSettings
    {
        public string GatewayBaseUri { get; set; }
        public string IdentityBaseUri { get; set; }
        public string PhotoStockUri { get; set; }
        public ServiceApi CatalogUri { get; set; }
    }

    public class ServiceApi
    {
        public string Path { get; set; }
    }
}
