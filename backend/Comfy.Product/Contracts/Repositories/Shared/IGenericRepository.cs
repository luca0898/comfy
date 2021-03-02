using Comfy.Product.Contracts.Shared;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Comfy.Product.Contracts.Repositories.Shared
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        Task<IEnumerable<TEntity>> FindAll(CancellationToken cancellationToken = default, int skip = 0, int take = 20);
        Task<TEntity> FindOne(int id, CancellationToken cancellationToken = default);

        Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default);
        Task SoftDelete(TEntity entity, CancellationToken cancellationToken = default);
        Task HardDelete(TEntity entity, CancellationToken cancellationToken = default);
    }
}
