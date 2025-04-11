using Kruger.Marketplace.Business.Validations.CadastroBasico;
using Microsoft.AspNetCore.Http;

namespace Kruger.Marketplace.Business.Models.CadastroBasico
{
    public class Produto : Entity
    {
        public int Estoque { get; private set; }
        public decimal Preco { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public string Imagem { get; private set; }
        public IFormFile FileUpload { get; private set; }
        public Guid CategoriaId { get; private set; }
        public Guid VendedorId { get; private set; }


        #region NAVIGATION PROPERTIES
        public Categoria Categoria { get; set; }
        public Vendedor Vendedor { get; set; }
        #endregion

        public Produto()
        {

        }

        public Produto(Guid id, int estoque, decimal preco, string nome, string descricao, string imagem, Guid categoriaId, Guid vendedorId)
        {
            Id = id;
            Estoque = estoque;
            Preco = preco;
            Nome = nome;
            Descricao = descricao;
            Imagem = imagem;
            CategoriaId = categoriaId;
            VendedorId = vendedorId;
        }

        public override bool IsValid()
        {
            ValidationResult = new ProdutoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
   
    }
}
