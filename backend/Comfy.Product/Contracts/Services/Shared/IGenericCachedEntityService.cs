using Comfy.Product.Contracts.Shared;

namespace Comfy.Product.Contracts.Services.Shared
{
    public interface IGenericCachedEntityService<TEntity> : IGenericEntityService<TEntity> where TEntity : IEntity
    {
    }
}
