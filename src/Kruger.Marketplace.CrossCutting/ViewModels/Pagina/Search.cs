namespace Kruger.Marketplace.CrossCutting.ViewModels.Pagina
{
    public class Search
    {
        public string ControllerToRedirect { get; set; }
        public string SearchTitle { get; set; }

        public Search()
        {

        }

        public Search(string controllerToRedirect, string searchTitle)
        {
            ControllerToRedirect = controllerToRedirect;
            SearchTitle = searchTitle;
        }
    }
}
