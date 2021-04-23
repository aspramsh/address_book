using AddressBook.Common.Includable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Common.Repositories
{
    public interface IEntityFrameworkGenericRepository<TEntity>
        where TEntity : class
    {
        void DetachLocal(Func<TEntity, bool> predicate);

        IQueryable<TEntity> Query();

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IIncludable<TEntity>, IIncludable> includes = default,
            CancellationToken cancellationToken = default);

        Task<TEntity> FirstOrDefaultTrackedAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IIncludable<TEntity>, IIncludable> includes = default,
            CancellationToken cancellationToken = default);

        Task<TEntity> FindAsync(CancellationToken cancellationToken = default, params object[] values);

        Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IIncludable<TEntity>, IIncludable> includes = default,
            string orderBy = default,
            CancellationToken cancellationToken = default);

        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task InsertRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default);

        void Update(TEntity entity);

        void UpdatePartially(TEntity entity, IEnumerable<string> updatedProperties, bool isAttached = false);

        void UpdateAttached(TEntity entity);

        void UpdateRange(List<TEntity> entities);

        void Delete(TEntity entity);

        void DeleteByRange(IEnumerable<TEntity> entities);

        Task UpsertAsync(TEntity entity, Expression<Func<TEntity, object>> match,
            CancellationToken cancellationToken = default);

        Task UpsertRangeAsync(IEnumerable<TEntity> entities,
            Expression<Func<TEntity, object>> match, CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
