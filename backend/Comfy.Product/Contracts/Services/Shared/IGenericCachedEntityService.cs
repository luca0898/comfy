using Comfy.Product.Contracts.Shared;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Comfy.Product.Contracts.Services.Shared
{
    public interface IGenericCachedEntityService<TEntity> : IGenericEntityService<TEntity>  where TEntity : IEntity
    {
    }
}
