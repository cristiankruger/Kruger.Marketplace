﻿using Kruger.Marketplace.Core.Business.Validations.CadastroBasico;
using Kruger.Marketplace.Core.Business.Models;

namespace Kruger.Marketplace.Core.Business.Models.CadastroBasico
{
    public class Categoria : Entity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }

        #region NAVIGATION PROPERTIES
        public Guid ProdutoId { get; set; }
        public IEnumerable<Produto> Produto { get; set; }
        #endregion

        public Categoria()
        {
            
        }

        public Categoria(Guid id, string nome, string descricao)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
        }
        
        public override bool IsValid()
        {
            ValidationResult = new CategoriaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    
    }
}
