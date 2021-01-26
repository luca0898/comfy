using Comfy.Product.Contracts.Shared;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Comfy.Product.Contracts.Services.Shared
{
    public interface IGenericEntityService<TEntity> where TEntity : IEntity
    {
        Task<IEnumerable<TEntity>> FindAll(CancellationToken cancellationToken, int skip = 0, int take = 20);
        Task<TEntity> GetOne(int id, CancellationToken cancellationToken);
        Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
