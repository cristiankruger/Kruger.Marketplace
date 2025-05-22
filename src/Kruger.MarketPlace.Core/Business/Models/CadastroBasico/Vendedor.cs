using Kruger.Marketplace.Core.Business.Validations.CadastroBasico;
using Kruger.Marketplace.Core.Business.Models;

namespace Kruger.Marketplace.Core.Business.Models.CadastroBasico
{
    public class Vendedor : Entity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        #region NAVIGATION PROPERTIES
        public Guid ProdutoId { get; set; }
        public IEnumerable<Produto> Produto { get; set; }
        #endregion

        public Vendedor()
        {
            
        }

        public Vendedor(Guid id, string nome, string email, string senha)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Senha = senha;
        }

        public override bool IsValid()
        {
            ValidationResult = new VendedorValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
