namespace Kruger.Marketplace.Business.Models.Settings
{
    public class AppSettings
    {
        public const string ConfigName = "AppSettings";

        public string Emissor { get; set; }
        public int ExpiracaoTokenMinutos { get; set; }
        public int ExpiracaoRefreshTokenMinutos { get; set; }
        public string Jwt { get; set; }
        public string[] ValidoEm { get; set; }
    }
}