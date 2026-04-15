using System.Data;
using CrossCutting;
using CrossCutting.Interfaces;

namespace Database;

public class UnitOfWorkFactory(DbContext dbContext) : IUnitOfWorkFactory
{
    public IUnitOfWork Create()
    {
        return new UnitOfWork(dbContext);
    }

    public IUnitOfWork Create(IsolationLevel isolationLevel)
    {
        return new UnitOfWork(dbContext);
    }
}