using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Threading;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class Repository <T> : IRepository<T> where T : BaseEntity
    {
        protected readonly Contexts.DefaultContext _context;

        public Repository(Contexts.DefaultContext context)
        {
            _context = context;
        }

        public async virtual Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>()
                .FirstOrDefaultAsync(s => s.Id == id && s.Status != Status.Inactive, cancellationToken);
        }

        public async virtual Task<T> AddAsync(T TEntity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Add(TEntity);
            await _context.SaveChangesAsync(cancellationToken);
            return TEntity;
        }

        public async virtual Task<T> UpdateAsync(T TEntity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Update(TEntity);
            await _context.SaveChangesAsync(cancellationToken);
            return TEntity;
        }

        public async virtual Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> where = null!, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null!)
        {
            var query = _context.Set<T>().AsQueryable();

            if (where is not null)
                query = query.Where(where);

            if (include is not null)
                query = include(query);

            return await query.FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
