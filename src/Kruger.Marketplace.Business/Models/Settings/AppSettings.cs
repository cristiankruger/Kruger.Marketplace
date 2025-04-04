namespace Kruger.Marketplace.Business.Models.Settings
{
    public class AppSettings
    {
        public string Emissor { get; set; }
        public int ExpiracaoTokenMinutos { get; set; }
        public int ExpiracaoRefreshTokenMinutos { get; set; }
        public string Secret { get; set; }
        public string[] ValidoEm { get; set; }       
    }
}