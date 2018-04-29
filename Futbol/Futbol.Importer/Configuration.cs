namespace Futbol.Importer
{
    public class Configuration
    {
        public AppSettingConfig AppSettings { get; set; }

        public ConnectionStringConfig ConnectionStrings { get; set; }

        public class AppSettingConfig
        {
            public string FootballDataApiUrl { get; set; }

            public string FootballDataApiKey { get; set; }
        }

        public class ConnectionStringConfig
        {
            public string FutbolDbConnection { get; set; }
        }
    }
}
