using Projeto.Business.Models;
using System.Linq.Expressions;

namespace Projeto.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity  
    {
        Task Adicionar(TEntity entity);
        Task<TEntity> ObterPorId(Guid id);
        Task<List<Entity>> ObterTodos();
        Task Atualizar(TEntity entity);
        Task Remover(Guid id);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();

    }
}
