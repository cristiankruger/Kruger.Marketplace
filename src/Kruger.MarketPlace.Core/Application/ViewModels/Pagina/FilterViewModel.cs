namespace Kruger.Marketplace.Core.Application.ViewModels.Pagina
{
    public class FilterViewModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Busca { get; set; } = string.Empty;
        public string OrderBy { get; set; } = string.Empty;
        public bool Desc { get; set; } = false;
    }
}
