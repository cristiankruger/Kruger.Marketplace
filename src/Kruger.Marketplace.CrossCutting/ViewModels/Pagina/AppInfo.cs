using Kruger.Marketplace.CrossCutting.Configurations;

namespace Kruger.Marketplace.CrossCutting.ViewModels.Pagina
{
    public class AppInfo
    {
        public string AppName { get; set; } = "Kruger.Marketplace";
        public string AppVersion { get; set; } = VersionInfo.GetVersionInfo();
    }
}
