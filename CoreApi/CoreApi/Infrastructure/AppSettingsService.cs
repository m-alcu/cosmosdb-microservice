namespace CoreApi.Infrastructure
{
    public class AppSettingsService
    {
        private readonly IConfiguration _configuration;
        private readonly Lazy<AppSettings> _appSettings;

        public AppSettingsService(IConfiguration configuration)
        {
            _configuration = configuration;
            _appSettings = new Lazy<AppSettings>(LoadAppSettings);
        }

        public AppSettings AppSettings => _appSettings.Value;

        private AppSettings LoadAppSettings()
        {
            AppSettings AppSettings = new AppSettings();
            AppSettings.EndpointUri = _configuration["EndpointUri"];
            AppSettings.PrimaryKey = _configuration["PrimaryKey"];
            return AppSettings;
        }
    }
}
