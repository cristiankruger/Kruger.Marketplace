using Kruger.Marketplace.Application.Extensions;
using Kruger.Marketplace.Application.ViewModels.CadastroBasico.Categoria;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Kruger.Marketplace.Application.ViewModels.CadastroBasico.Produto
{
    public class ProdutoViewModel : EntityViewModel
    {
        [DisplayName("Produto")]
        [Required(ErrorMessage = "Informe o campo Nome da Categoria.")]
        [MaxLength(100, ErrorMessage = "O campo Nome da Categoria deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Informe o campo Descrição.")]
        [MaxLength(500, ErrorMessage = "O campo Descrição deve ter no máximo 500 caracteres.")]
        public string Descricao { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Informe uma quantidade maior que zero.")]
        [DisplayName("Estoque")]
        [Required(ErrorMessage = "Informe a quantidade em estoque.")]
        public int Estoque { get; set; }

        [DisplayName("Preço")]
        [Range(0.01, int.MaxValue, ErrorMessage = "Informe um Preço maior que zero.")]
        [Required(ErrorMessage = "Informe um Preço.")]
        public decimal Preco { get; set; }

        [GuidNotEmpty("Categorias")]
        [DisplayName("Categorias")]
        [Required(ErrorMessage = "Selecione uma categoria.")]
        public Guid CategoriaId { get; set; }

        [DisplayName("VendedorId")]
        public Guid VendedorId { get; set; }

        [DataType(DataType.Upload)]
        [AllowedExtensions([".jpg", ".jpeg", ".png"])]
        [FileSize(20)]
        [DisplayName("Selecione uma imagem")]
        public IFormFile FileUpload { get; set; }
        
        public string Imagem { get; set; }
        
        //public string ImageUri { get; set; }

        //[DisplayName("Imagem")]
        //public string ImageDisplayName { get; set; }

        public string Categoria { get; set; }

        public IEnumerable<CategoriaViewModel> Categorias { get; set; }

        public void SetVendedorId(Guid id)
        {
            VendedorId = id;
        }

        public void SetImageProperties(string imagePath, string imageName)
        {
            Imagem = FileUpload is not null ? $"{Id}_{FileUpload.FileName}" : !string.IsNullOrEmpty(Imagem) ? Imagem : imageName;
            //ImageUri = $"{imagePath}{Imagem}";
            //ImageDisplayName = Imagem[37..];
        }
    }
}
