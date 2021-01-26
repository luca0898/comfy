using Comfy.Product.Contracts.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Comfy.Product.Contracts.Repositories.Shared
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        Task<IEnumerable<TEntity>> FindAll(CancellationToken cancellationToken, int skip = 0, int take = 20);
        Task<TEntity> FindOne(int id, CancellationToken cancellationToken);

        Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken);
        Task SoftDelete(TEntity entity, CancellationToken cancellationToken);
        Task HardDelete(TEntity entity, CancellationToken cancellationToken);
    }
}
