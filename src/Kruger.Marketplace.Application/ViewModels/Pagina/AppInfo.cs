using Kruger.Marketplace.Application.Configurations;

namespace Kruger.Marketplace.Application.ViewModels.Pagina
{
    public class AppInfo
    {
        public string AppName { get; set; } = "Kruger.Marketplace";
        public string AppVersion { get; set; } = VersionInfo.GetVersionInfo();
    }
}
