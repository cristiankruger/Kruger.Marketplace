namespace Kruger.Marketplace.Business.Models.Settings
{
    public class ArquivoSettings
    {
        public string BasePath { get; set; }
        public string Container{ get; set; }
        public string DefaultImage { get; set; }

        public ArquivoSettings()
        {
            
        }

        public ArquivoSettings(string basePath, string container, string defaultImage)
        {
            BasePath = basePath;
            Container = container;
            DefaultImage = defaultImage;
        }
    }
}
