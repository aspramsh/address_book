using AddressBook.Common.Includable;
using AddressBook.Common.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Common.Services
{
    public class GenericService<TModel, TEntity> : IGenericService<TModel, TEntity>
        where TEntity : class
        where TModel : class
    {
        protected readonly IMapper Mapper;
        protected readonly ILogger Logger;
        protected IEntityFrameworkGenericRepository<TEntity> EntityRepository { get; }

        public GenericService(IMapper mapper,
            ILoggerFactory loggerFactory,
            IEntityFrameworkGenericRepository<TEntity> entityRepository)
        {
            Mapper = mapper;
            Logger = loggerFactory.CreateLogger<GenericService<TModel, TEntity>>();
            EntityRepository = entityRepository;
        }

        public void DetachLocal(Func<TEntity, bool> predicate)
        {
            EntityRepository.DetachLocal(predicate);
        }

        #region selects

        public virtual async Task<TModel> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IIncludable<TEntity>, IIncludable> includes = default,
            bool isTracked = false,
            CancellationToken cancellationToken = default)
        {
            var query = EntityRepository.Query();

            if (!isTracked)
            {
                query = query.AsNoTracking();
            }
            else
            {
                query = query.AsTracking();
            }

            var model = await query.Where(predicate)
                .IncludeMultiple(includes)
                .FirstOrDefaultAsync(cancellationToken);

            return Mapper.Map<TModel>(model);
        }

        public virtual async Task<List<TModel>> GetAllAsync(
            Func<IIncludable<TEntity>, IIncludable> includes = default,
            string orderBy = null,
            CancellationToken cancellationToken = default)
        {
            var query = EntityRepository.Query();

            if (!string.IsNullOrEmpty(orderBy))
            {
                query = query.OrderBy(orderBy);
            }

            var entities = await query
                .AsNoTracking()
                .IncludeMultiple(includes)
                .ToListAsync(cancellationToken);

            return Mapper.Map<List<TModel>>(entities);
        }

        public virtual async Task<List<TModel>> FindByAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IIncludable<TEntity>, IIncludable> includes = default,
            string orderBy = default,
            CancellationToken cancellationToken = default)
        {
            var query = EntityRepository.Query();

            if (!string.IsNullOrEmpty(orderBy))
            {
                query = query.OrderBy(orderBy);
            }

            var entities = await query
                .AsNoTracking()
                .Where(predicate)
                .IncludeMultiple(includes)
                .ToListAsync(cancellationToken);

            return Mapper.Map<List<TModel>>(entities);
        }

        public virtual async Task<long> CountAsync(Expression<Func<TEntity, bool>> whereCondition,
            CancellationToken cancellationToken = default)
        {
            return await EntityRepository.Query()
                .AsTracking()
                .Where(whereCondition)
                .CountAsync(cancellationToken: cancellationToken);
        }

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereCondition,
            CancellationToken cancellationToken = default)
        {
            return EntityRepository.Query()
                .AsNoTracking()
                .AnyAsync(whereCondition, cancellationToken);
        }

        #endregion

        #region updates

        public virtual void Update(TModel model)
        {
            var dataEntity = Mapper.Map<TEntity>(model);
            EntityRepository.Update(dataEntity);
        }

        public virtual void UpdateAttached(TModel model)
        {
            var dataEntity = Mapper.Map<TEntity>(model);
            EntityRepository.UpdateAttached(dataEntity);
        }

        public virtual void UpdateRange(List<TModel> models)
        {
            var entities = Mapper.Map<List<TEntity>>(models);
            EntityRepository.UpdateRange(entities);
        }

        public virtual void UpdatePartially(TModel model, List<string> updatedProperties,
            bool isAttached = false)
        {
            var entity = Mapper.Map<TEntity>(model);
            EntityRepository.UpdatePartially(entity, updatedProperties, isAttached);
        }

        #endregion

        #region inserts

        public virtual async Task<TModel> InsertAsync(TModel model,
            CancellationToken cancellationToken = default)
        {
            var dataEntity = Mapper.Map<TEntity>(model);
            var result = await EntityRepository.InsertAsync(dataEntity, cancellationToken);

            return Mapper.Map<TModel>(result);
        }

        public virtual async Task InsertRangeAsync(List<TModel> models,
            CancellationToken cancellationToken = default)
        {
            var dataEntities = Mapper.Map<List<TEntity>>(models);
            await EntityRepository.InsertRangeAsync(dataEntities, cancellationToken);
        }

        #endregion

        #region deletes

        public virtual void Delete(TModel entity)
        {
            var dataEntity = Mapper.Map<TModel, TEntity>(entity);
            EntityRepository.Delete(dataEntity);
        }

        public virtual void DeleteRange(List<TModel> models)
        {
            var dataEntities = Mapper.Map<List<TEntity>>(models);
            EntityRepository.DeleteByRange(dataEntities);
        }

        #endregion

        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await EntityRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
