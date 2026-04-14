using Comfy.Product.Contracts.Repositories.Shared;
using Comfy.Product.Contracts.Services.Shared;
using Comfy.Product.Contracts.Shared;
using Comfy.SystemObjects;
using Comfy.SystemObjects.Exceptions;
using Comfy.SystemObjects.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Comfy.Services.Shared
{
    public abstract class GenericEntityService<TEntity> : IGenericEntityService<TEntity> where TEntity : class, IEntity
    {
        protected readonly IUnitOfWorkFactory<UnitOfWork> _uow;
        protected readonly IGenericRepository<TEntity> _repository;

        public GenericEntityService(IGenericRepository<TEntity> TEntityRepository, IUnitOfWorkFactory<UnitOfWork> uow)
        {
            _repository = TEntityRepository;
            _uow = uow;
        }

        public virtual async Task<IEnumerable<TEntity>> FindAllAsync(CancellationToken cancellationToken = default, int skip = 0, int take = 20)
        {
            return await _repository.FindAll(cancellationToken, skip, take);
        }

        public virtual async Task<TEntity> GetOneAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _repository.FindOne(id, cancellationToken);
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            using IUnitOfWork uow = _uow.Create();

            TEntity result = await _repository.Create(entity, cancellationToken);

            await uow.CommitAsync(cancellationToken);

            return result;
        }

        public virtual async Task<TEntity> UpdateAsync(int id, TEntity entity, CancellationToken cancellationToken = default)
        {
            using IUnitOfWork uow = _uow.Create();

            TEntity existingEntity = await _repository.FindOne(id, cancellationToken);

            if (existingEntity == null || existingEntity.Id <= 0)
            {
                throw new ComfyApplicationException($"{typeof(TEntity).Name} {id} not found", HttpStatusCode.NotFound);
            }

            TEntity result = await _repository.Update(entity, cancellationToken);

            await uow.CommitAsync(cancellationToken);

            return result;
        }

        public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            using IUnitOfWork uow = _uow.Create();

            TEntity entity = await _repository.FindOne(id, cancellationToken);

            if (entity == null || entity.Id <= 0)
            {
                throw new ComfyApplicationException($"{typeof(TEntity).Name} {id} not found", HttpStatusCode.NotFound);
            }

            await _repository.SoftDelete(entity, cancellationToken);
            await uow.CommitAsync(cancellationToken);
        }
    }
}
