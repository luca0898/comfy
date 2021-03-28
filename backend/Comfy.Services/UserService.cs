using Comfy.Product.Contracts.Repositories;
using Comfy.Product.Contracts.Services;
using Comfy.Product.Entities;
using Comfy.Services.Shared;
using Comfy.SystemObjects;
using Comfy.SystemObjects.Interfaces;

namespace Comfy.Service
{
    public class UserService : GenericCachedEntityService<User>, IUserService
    {
        public UserService(
            ICurrentSessionUser currentSessionUser,
            ICacheProvider cacheProvider,
            IUserRepository repository,
            IUnitOfWorkFactory<UnitOfWork> uow) : base(currentSessionUser, cacheProvider, repository, uow)
        {
        }
    }
}
