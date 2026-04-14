using Comfy.Db.SQL.Repositories.Shared;
using Comfy.Product.Contracts.Repositories;
using Comfy.Product.Entities;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Db.SQL.Repositories
{
    public class UserRepository : GenericRelationalRepository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
