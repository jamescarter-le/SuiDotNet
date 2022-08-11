namespace SuiDotNet.Client
{
    public class SuiClientSettings
    {
        public string BaseUri { get; set; }
        public Dictionary<string, string> PackageIdOverrides { get; set; }

        public SuiClientSettings()
        {
            BaseUri = string.Empty;
            PackageIdOverrides = new();
        }

        public SuiClientSettings(string baseUri)
        {
            BaseUri = baseUri;
            PackageIdOverrides = new();
        }

        public SuiClientSettings(string baseUri, Dictionary<string, string> packageIdOverrides)
        {
            BaseUri = baseUri;
            PackageIdOverrides = packageIdOverrides;
        }
    }
}
