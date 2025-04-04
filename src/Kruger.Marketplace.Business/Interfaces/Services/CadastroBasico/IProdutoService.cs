﻿using Kruger.Marketplace.Business.Models.CadastroBasico;
using System.Linq.Expressions;

namespace Kruger.Marketplace.Business.Interfaces.Services.CadastroBasico
{
    public interface IProdutoService : IDisposable
    {
        Task<IEnumerable<Produto>> GetAll(Expression<Func<Produto, bool>> predicate, Expression<Func<Produto, object>> orderBy, int pageNumber, int pageSize, bool desc);
        Task<Produto> GetById(Guid id);
        Task<bool> Add(Produto produto);
        Task<bool> Update(Produto produto);
        Task<bool> Delete(Guid id);
        Task Complete();
    }
}
