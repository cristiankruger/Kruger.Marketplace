using Kruger.Marketplace.Business.Models.CadastroBasico;
using Kruger.Marketplace.CrossCutting.ViewModels.CadastroBasico.Categoria;
using Kruger.Marketplace.CrossCutting.ViewModels.CadastroBasico.Produto;
using Kruger.Marketplace.CrossCutting.ViewModels.Pagina;
using LinqKit;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace Kruger.Marketplace.CrossCutting.Expressions
{
    public static class ControllerExpression
    {
        public static Expression<Func<Categoria, bool>> GetFilterExpression(CategoriaFilter filter)
        {
            var predicate = PredicateBuilder.New<Categoria>(true);

            if (!string.IsNullOrEmpty(filter.Busca))
                predicate.And(c => c.Nome.Contains(filter.Busca) ||
                                   c.Descricao.Contains(filter.Busca));

            return predicate;
        }

        public static Expression<Func<Categoria, object>> GetOrderByExpression(CategoriaFilter filter)
        {
            filter.OrderBy ??= string.Empty;

            return filter.OrderBy.ToLower() switch
            {
                "descricao" => c => c.Descricao,
                "nome" or
                _ => c => c.Nome,
            };
        }

        public static Expression<Func<Produto, bool>> GetFilterExpression(ProdutoFilter filter)
        {
            var predicate = PredicateBuilder.New<Produto>(true);

            if (!string.IsNullOrEmpty(filter.Busca))
                predicate.And(c => c.Nome.Contains(filter.Busca) ||
                                   c.Descricao.Contains(filter.Busca));

            return predicate;
        }

        public static Expression<Func<Produto, object>> GetOrderByExpression(ProdutoFilter filter)
        {
            filter.OrderBy ??= string.Empty;

            return filter.OrderBy.ToLower() switch
            {
                "descricao" => p => p.Descricao,
                "preco" => p => p.Preco,
                "estoque" => p => p.Estoque,
                "categoria" => p => p.Categoria.Nome,
                "vendedor" => p => p.Vendedor.Nome,
                "nome" or
                _ => p => p.Nome,
            };

        }
    }
}
