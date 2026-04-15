using Domain.Contracts.Shared;

namespace Domain.Contracts.Repositories.Shared;

public interface IGenericRepository<TEntity> where TEntity : class, IEntity
{
    Task<IList<TEntity>> FindAll(int skip = 0, int take = 20, CancellationToken cancellationToken = default);
    Task<TEntity?> FindOne(int id, CancellationToken cancellationToken = default);
    Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity?> Update(TEntity entity, CancellationToken cancellationToken = default);
    Task SoftDelete(TEntity entity, CancellationToken cancellationToken = default);
    Task HardDelete(TEntity entity, CancellationToken cancellationToken = default);
}