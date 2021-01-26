using Comfy.SystemObjects;
using Comfy.SystemObjects.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Comfy.Db.SQL
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory<UnitOfWork>
    {
        private readonly DbContext _dbContext;
        public UnitOfWorkFactory(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IUnitOfWork Create()
        {
            return new UnitOfWork(_dbContext);
        }

        public IUnitOfWork Create(IsolationLevel isolationLevel)
        {
            return new UnitOfWork(_dbContext, isolationLevel);
        }
    }
}
