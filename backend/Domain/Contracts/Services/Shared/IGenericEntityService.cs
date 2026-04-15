using Domain.Contracts.Shared;

namespace Domain.Contracts.Services.Shared;

public interface IGenericEntityService<TEntity> where TEntity : IEntity
{
    Task<IList<TEntity>> FindAllAsync(int skip = 0, int take = 20, CancellationToken cancellationToken = default);
    Task<TEntity?> GetOneAsync(int id, CancellationToken cancellationToken = default);
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity?> UpdateAsync(int id, TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}