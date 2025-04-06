using Kruger.Marketplace.CrossCutting.Extensions;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Kruger.Marketplace.CrossCutting.ViewModels.CadastroBasico.Produto
{
    public class ProdutoViewModel : EntityViewModel
    {
        [DisplayName("Produto")]
        [Required(ErrorMessage = "Informe o campo Nome da Categoria.")]
        [MaxLength(100, ErrorMessage = "O campo Nome da Categoria deve ter no máximo 100 caracteres.")]
        public string Nome{ get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage ="Informe o campo Descrição.")]
        [MaxLength(500, ErrorMessage = "O campo Descrição deve ter no máximo 500 caracteres.")]
        public string Descricao { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Informe uma quantidade maior que zero.")]
        [DisplayName("Estoque")]
        public int Estoque { get;  set; }

        [DisplayName("Preço")]
        [Range(0.01, int.MaxValue, ErrorMessage = "Informe um Preço maior que zero.")]
        public decimal Preco { get;  set; }

        [GuidNotEmpty("Categorias")]
        [DisplayName("Categorias")]
        public Guid CategoriaId { get;  set; }

        [DisplayName("Vendedor")]
        public Guid VendedorId { get; set; }

        public string Imagem { get;  set; }
        
        [DataType(DataType.Upload)]
        [AllowedExtensions([".jpg", ".jpeg", ".png"])]
        [FileSize(5)]
        public IFormFile FileUpload { get;  set; }

        public string Categoria { get; set; }

    }
}
