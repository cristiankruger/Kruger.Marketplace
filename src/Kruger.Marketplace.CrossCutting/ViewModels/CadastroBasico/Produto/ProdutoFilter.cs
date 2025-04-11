using Kruger.Marketplace.CrossCutting.ViewModels.Pagina;

namespace Kruger.Marketplace.CrossCutting.ViewModels.CadastroBasico.Produto
{
    public class ProdutoFilter : FilterViewModel
    {
        public Guid CategoriaId { get; set; }
        public Guid VendedorId { get; set; }

        public void SetVendedorId(Guid id)
        {
            VendedorId = id;
        }
    }
}
