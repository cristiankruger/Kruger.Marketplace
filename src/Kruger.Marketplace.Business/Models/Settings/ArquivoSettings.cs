namespace Kruger.Marketplace.Business.Models.Settings
{
    public class ArquivoSettings
    {
        public string BasePath { get; set; }
        public string DefaultImage { get; set; }

        public ArquivoSettings()
        {
            
        }

        public ArquivoSettings(string basePath, string defaultImage)
        {
            BasePath = basePath;
            DefaultImage = defaultImage;
        }
    }
}
