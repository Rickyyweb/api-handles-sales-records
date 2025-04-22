using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IRepository <T> where T : BaseEntity
    {
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> where = null!, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null!);

        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<T> AddAsync(T TEntity, CancellationToken cancellationToken = default);

        Task<T> UpdateAsync(T TEntity, CancellationToken cancellationToken = default);

        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    }
}
