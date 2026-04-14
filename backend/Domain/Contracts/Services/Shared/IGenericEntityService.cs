using Comfy.Product.Contracts.Shared;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Comfy.Product.Contracts.Services.Shared
{
    public interface IGenericEntityService<TEntity> where TEntity : IEntity
    {
        Task<IEnumerable<TEntity>> FindAllAsync(CancellationToken cancellationToken = default, int skip = 0, int take = 20);
        Task<TEntity> GetOneAsync(int id, CancellationToken cancellationToken = default);
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> UpdateAsync(int id, TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
