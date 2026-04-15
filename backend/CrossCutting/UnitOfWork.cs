using CrossCutting.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CrossCutting;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly bool _alreadyInTransaction;
    private readonly DbContext _dbContext;
    private bool _disposed;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(DbContext dbContext)
    {
        if (dbContext.Database.CurrentTransaction == null)
            _transaction = dbContext.Database.BeginTransaction();
        else
            _alreadyInTransaction = true;

        _dbContext = dbContext;
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        if (!_alreadyInTransaction && _transaction != null)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _transaction.CommitAsync(cancellationToken);
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!disposing || _disposed) return;

        if (_transaction != null)
        {
            _transaction.Dispose();
            _transaction = null;
        }

        _disposed = true;
    }
}