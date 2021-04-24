using AddressBook.Common.Includable;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Common.Services
{
    public interface IGenericService<TModel, TEntity>
        where TModel : class
    {
        #region selects

        void DetachLocal(Func<TEntity, bool> predicate);

        Task<TModel> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IIncludable<TEntity>, IIncludable> includes = default,
            bool isTracked = false,
            CancellationToken cancellationToken = default);

        Task<List<TModel>> GetAllAsync(Func<IIncludable<TEntity>, IIncludable> includes = default,
            string orderBy = default,
            CancellationToken cancellationToken = default);

        Task<List<TModel>> FindByAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IIncludable<TEntity>, IIncludable> includes = default,
            string orderBy = default,
            CancellationToken cancellationToken = default);

        Task<long> CountAsync(Expression<Func<TEntity, bool>> whereCondition,
            CancellationToken cancellationToken = default);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereCondition,
            CancellationToken cancellationToken = default);

        #endregion

        #region updates

        void Update(TModel model);

        void UpdateAttached(TModel model);

        void UpdateRange(List<TModel> models);

        void UpdatePartially(TModel entity, List<string> updatedProperties, bool isAttached = false);

        #endregion

        #region Inserts

        Task<TModel> InsertAsync(TModel model, CancellationToken cancellationToken = default);

        Task InsertRangeAsync(List<TModel> model, CancellationToken cancellationToken = default);

        #endregion

        #region delete

        void Delete(TModel entity);

        void DeleteRange(List<TModel> models);

        #endregion

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
