using Comfy.Product.Contracts.Repositories.Shared;
using Comfy.Product.Contracts.Services;
using Comfy.Product.Contracts.Services.Shared;
using Comfy.Product.Contracts.Shared;
using Comfy.SystemObjects;
using Comfy.SystemObjects.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Comfy.Services.Shared
{
    public abstract class GenericCachedEntityService<TEntity> : GenericEntityService<TEntity>, IGenericCachedEntityService<TEntity> where TEntity : class, IEntity
    {
        public ICurrentSessionUser _currentSessionUser { get; private set; }
        protected readonly ICacheProvider _cacheProvider;

        public GenericCachedEntityService(
            ICurrentSessionUser currentSessionUser,
            ICacheProvider cacheProvider,
            IGenericRepository<TEntity> TEntityRepository,
            IUnitOfWorkFactory<UnitOfWork> uow) : base(TEntityRepository, uow)
        {
            _currentSessionUser = currentSessionUser;
            _cacheProvider = cacheProvider;
        }

        public override async Task<IEnumerable<TEntity>> FindAllAsync(CancellationToken cancellationToken = default, int skip = 0, int take = 20)
        {
            string key = $"{GetCacheKeyPrefix()}:{ typeof(TEntity).Name }:FindAll:Skip={skip}:Take={take}";

            IEnumerable<TEntity> dataFromCache = await _cacheProvider.FindCacheAsync<IEnumerable<TEntity>>(key, cancellationToken);

            if (dataFromCache == null)
            {
                IEnumerable<TEntity> dataFromDb = await base.FindAllAsync(cancellationToken, skip, take);

                if (dataFromDb == null || !dataFromDb.Any())
                {
                    return default;
                }

                await _cacheProvider.CreateAsync(key, dataFromDb, null, cancellationToken);

                return dataFromDb;
            }

            return dataFromCache;
        }

        public override async Task<TEntity> GetOneAsync(int id, CancellationToken cancellationToken = default)
        {
            string key = $"{GetCacheKeyPrefix()}:{ typeof(TEntity).Name }:GetOne={id}";

            TEntity dataFromCache = await _cacheProvider.FindCacheAsync<TEntity>(key, cancellationToken);

            if (dataFromCache == null)
            {
                TEntity dataFromDb = await base.GetOneAsync(id, cancellationToken);

                if (dataFromDb == null)
                {
                    return default;
                }

                await _cacheProvider.CreateAsync(key, dataFromDb, null, cancellationToken);

                return dataFromDb;
            }

            return dataFromCache;
        }

        public override async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            TEntity newRecord = await base.CreateAsync(entity, cancellationToken);

            if (newRecord != null)
            {
                await _cacheProvider.InvalidateCacheAsync<TEntity>();
            }

            return newRecord;
        }

        public override async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            TEntity record = await base.UpdateAsync(entity, cancellationToken);

            if (record != null)
            {
                await _cacheProvider.InvalidateCacheAsync<TEntity>();
            }

            return record;
        }

        public override async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await base.DeleteAsync(id, cancellationToken);

            await _cacheProvider.InvalidateCacheAsync<TEntity>();
        }

        private string GetCacheKeyPrefix()
        {
            if (_currentSessionUser != null && string.IsNullOrEmpty(_currentSessionUser.Id) == false)
            {
                return $"{_currentSessionUser.Id}";
            }
            else
            {
                return "Anonymous";
            }
        }
    }
}
