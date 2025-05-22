using Kruger.Marketplace.Core.Application.Configurations;

namespace Kruger.Marketplace.Core.Application.ViewModels.Pagina
{
    public class AppInfo
    {
        public string AppName { get; set; } = "Kruger.Marketplace";
        public string AppVersion { get; set; } = VersionInfo.GetVersionInfo();
    }
}
