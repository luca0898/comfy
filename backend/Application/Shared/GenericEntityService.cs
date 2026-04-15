using CrossCutting.Exceptions;
using CrossCutting.Interfaces;
using Domain.Contracts.Repositories.Shared;
using Domain.Contracts.Services.Shared;
using Domain.Contracts.Shared;

namespace Application.Shared;

public abstract class GenericEntityService<TEntity>(
    IGenericRepository<TEntity> repository,
    IUnitOfWorkFactory unitOfWorkFactory)
    : IGenericEntityService<TEntity>
    where TEntity : class, IEntity
{
    public virtual async Task<IList<TEntity>> FindAllAsync(int skip = 0, int take = 20,
        CancellationToken cancellationToken = default)
    {
        return await repository.FindAll(skip, take, cancellationToken);
    }

    public virtual async Task<TEntity?> GetOneAsync(int id, CancellationToken cancellationToken = default)
    {
        return await repository.FindOne(id, cancellationToken);
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        using var uow = unitOfWorkFactory.Create();

        var result = await repository.Create(entity, cancellationToken);

        await uow.CommitAsync(cancellationToken);

        return result;
    }

    public virtual async Task<TEntity?> UpdateAsync(int id, TEntity entity,
        CancellationToken cancellationToken = default)
    {
        using var uow = unitOfWorkFactory.Create();

        var existingEntity = await repository.FindOne(id, cancellationToken);

        if (existingEntity == null || existingEntity.Id <= 0)
            throw new ComfyApplicationException($"{typeof(TEntity).Name} {id} not found");

        var result = await repository.Update(entity, cancellationToken);

        await uow.CommitAsync(cancellationToken);

        return result;
    }

    public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        using var uow = unitOfWorkFactory.Create();

        var entity = await repository.FindOne(id, cancellationToken);

        if (entity == null)
            throw new ComfyApplicationException($"{typeof(TEntity).Name} {id} not found");

        await repository.SoftDelete(entity, cancellationToken);
        await uow.CommitAsync(cancellationToken);
    }
}