using Comfy.Product.Contracts.Repositories.Shared;
using Comfy.Product.Contracts.Shared;
using Comfy.SystemObjects;
using Comfy.SystemObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Comfy.Services.Shared
{
    public abstract class GenericEntityService<TEntity> where TEntity : class, IEntity
    {
        private readonly IUnitOfWorkFactory<UnitOfWork> _uow;
        private readonly IGenericRepository<TEntity> _repository;

        public GenericEntityService(IGenericRepository<TEntity> TEntityRepository, IUnitOfWorkFactory<UnitOfWork> uow)
        {
            _repository = TEntityRepository;
            _uow = uow;
        }

        public async Task<IEnumerable<TEntity>> FindAll(CancellationToken cancellationToken, int skip = 0, int take = 20)
        {
            return await _repository.FindAll(cancellationToken, skip, take);
        }

        public async Task<TEntity> GetOne(int id, CancellationToken cancellationToken)
        {
            return await _repository.FindOne(id, cancellationToken);
        }

        public async Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken)
        {
            using (var uow = _uow.Create())
            {
                TEntity result = await _repository.Create(entity, cancellationToken);
                await uow.CommitAsync();

                return result;
            }
        }
        public async Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken)
        {
            using (var uow = _uow.Create())
            {
                TEntity schedule = await _repository.FindOne(entity.Id, cancellationToken);

                if (schedule != null && schedule.Id > 0)
                {
                    var result = await _repository.Update(entity, cancellationToken);
                    await uow.CommitAsync();

                    return result;
                }
                else
                {
                    throw new ArgumentException($"{typeof(TEntity).Name} {entity.Id} not found");
                }
            }
        }
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            using (var uow = _uow.Create())
            {
                TEntity TEntity = await _repository.FindOne(id, cancellationToken);

                if (TEntity != null && TEntity.Id > 0)
                {
                    await _repository.SoftDelete(TEntity, cancellationToken);
                    await uow.CommitAsync();
                }
                else
                {
                    throw new ArgumentException($"{typeof(TEntity).Name} {id} not found");
                }
            }
        }
    }
}
