namespace Kruger.Marketplace.Core.Application.ViewModels.Pagina
{
    public class Paged
    {
        public string ControllerToRedirect { get; set; }
        public int TotalRecords { get; set; }
        public FilterViewModel Filter { get; set; }

        public Paged()
        {
            
        }

        public Paged(string controllerToRedirect, int totalRecords, FilterViewModel filter)
        {
            ControllerToRedirect = controllerToRedirect;
            TotalRecords = totalRecords;
            Filter = filter;
        }

    }
}
