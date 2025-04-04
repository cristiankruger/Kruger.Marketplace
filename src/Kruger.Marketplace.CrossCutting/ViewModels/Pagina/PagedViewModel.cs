using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kruger.Marketplace.CrossCutting.ViewModels.Pagina
{
    public class PagedViewModel<T> : Paged where T : class
    {
        public IEnumerable<T> PagedData { get; set; }
        public List<SelectListItem> PageSizeList { get; set; }
    }
}
