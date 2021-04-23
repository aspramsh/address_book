using AddressBook.Common.Includable;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Common.Repositories
{
    public abstract class EntityFrameworkGenericRepository<TEntity> : IEntityFrameworkGenericRepository<TEntity>
        where TEntity : class, new()
    {
        protected readonly ILogger Logger;
        protected readonly DbContext DbContext;

        protected EntityFrameworkGenericRepository(DbContext dbContext, ILoggerFactory loggerFactory)
        {
            DbContext = dbContext;
            Logger = loggerFactory.CreateLogger<EntityFrameworkGenericRepository<TEntity>>();
        }

        public void DetachLocal(Func<TEntity, bool> predicate)
        {
            var local = DbContext.Set<TEntity>()
                .Local
                .FirstOrDefault(predicate);

            if (local != default)
            {
                DbContext.Entry(local).State = EntityState.Detached;
            }
        }

        public IQueryable<TEntity> Query()
        {
            return DbContext.Set<TEntity>().AsQueryable();
        }

        /// <inheritdoc />
        /// <summary>
        /// Get by params
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken = default,
            params object[] values)
        {
            return await DbContext.FindAsync<TEntity>(values, cancellationToken: cancellationToken);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get one by conditions (Not tracked)
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IIncludable<TEntity>, IIncludable> includes = default,
            CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<TEntity>()
                .AsNoTracking()
                .IncludeMultiple(includes)
                .FirstOrDefaultAsync(predicate, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Get one by conditions (Tracked)
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FirstOrDefaultTrackedAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IIncludable<TEntity>, IIncludable> includes = default,
            CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<TEntity>()
                .AsTracking()
                .IncludeMultiple(includes)
                .FirstOrDefaultAsync(predicate, cancellationToken: cancellationToken);
        }

        /// <inheritdoc />
        /// <summary>
        /// Search by conditions
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <param name="orderBy"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IIncludable<TEntity>, IIncludable> includes = default,
            string orderBy = default,
            CancellationToken cancellationToken = default)
        {
            var query = DbContext.Set<TEntity>().AsNoTracking();

            if (predicate != default)
            {
                query = query.Where(predicate);
            }

            if (includes != default)
            {
                query = query.IncludeMultiple(includes);
            }

            if (orderBy != default)
            {
                query = query.OrderBy(orderBy);
            }

            return await query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Insert one item
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> InsertAsync(TEntity entity,
            CancellationToken cancellationToken = default)
        {
            var data = await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
            return data.Entity;
        }

        /// <inheritdoc />
        /// <summary>
        /// Insert by range
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task InsertRangeAsync(List<TEntity> entities,
            CancellationToken cancellationToken = default)
        {
            await DbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
        }

        /// <inheritdoc />
        /// <summary>
        /// Update one item
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            DbContext.Set<TEntity>().Update(entity);
        }

        /// <summary>
        /// Update only changed columns
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="updatedProperties"></param>
        /// <param name="isAttachable"></param>
        public virtual void UpdatePartially(TEntity entity, IEnumerable<string> updatedProperties,
            bool isAttachable = false)
        {
            if (isAttachable)
            {
                DbContext.Attach(entity);
            }

            var keyNames = DbContext.Model
                .FindEntityType(typeof(TEntity))
                .FindPrimaryKey().Properties
                .Select(x => x.Name)
                .ToArray();

            foreach (var item in updatedProperties)
            {
                if (!keyNames.Contains(item))
                {
                    DbContext.Entry(entity).Property(item).IsModified = true;
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Update one item
        /// </summary>
        /// <param name="entity"></param>
        public virtual void UpdateAttached(TEntity entity)
        {
            DbContext.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <inheritdoc />
        /// <summary>
        /// Update by range
        /// </summary>
        /// <param name="entities"></param>
        public virtual void UpdateRange(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                DbContext.Entry(entity).State = EntityState.Modified;
            }

            DbContext.Set<TEntity>().UpdateRange(entities);
        }

        /// <summary>
        /// Delete one
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Deleted;
            DbContext.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// Delete by range
        /// </summary>
        /// <param name="entities"></param>
        public virtual void DeleteByRange(IEnumerable<TEntity> entities)
        {
            var removeRange = entities as TEntity[] ?? entities.ToArray();

            foreach (var entity in removeRange)
            {
                DbContext.Entry(entity).State = EntityState.Deleted;
            }

            DbContext.Set<TEntity>().RemoveRange(removeRange);
        }

        public virtual async Task UpsertAsync(TEntity entity, Expression<Func<TEntity, object>> match,
            CancellationToken cancellationToken = default)
        {
            await DbContext.Set<TEntity>().Upsert(entity)
                .On(match)
                .RunAsync(cancellationToken);
        }

        public virtual async Task UpsertRangeAsync(IEnumerable<TEntity> entities,
            Expression<Func<TEntity, object>> match, CancellationToken cancellationToken = default)
        {
            await DbContext.Set<TEntity>().UpsertRange(entities)
                .On(match)
                .RunAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
