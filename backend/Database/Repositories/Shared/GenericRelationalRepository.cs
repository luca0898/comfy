using Domain.Contracts.Repositories.Shared;
using Domain.Contracts.Shared;

namespace Database.Repositories.Shared;

public abstract class GenericRelationalRepository<TEntity>(DbContext dbContext)
    : IGenericRepository<TEntity> where TEntity : class, IEntity
{
    private readonly DbSet<TEntity> _dbSetEntity = dbContext.Set<TEntity>();

    public async Task<IList<TEntity>> FindAll(
        int skip = 0,
        int take = 20,
        CancellationToken cancellationToken = default)
    {
        return await _dbSetEntity
            .AsQueryable()
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> FindOne(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSetEntity
            .AsNoTracking()
            .FirstOrDefaultAsync(schedule => schedule.Id == id, cancellationToken);
    }

    public async Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken = default)
    {
        var record = await _dbSetEntity.AddAsync(entity, cancellationToken);

        return record.Entity;
    }

    public async Task<TEntity?> Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        var record = _dbSetEntity.Update(entity);

        return await Task.FromResult(record.Entity);
    }

    public Task SoftDelete(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.Deleted = true;
        _dbSetEntity.Update(entity);

        return Task.CompletedTask;
    }

    public Task HardDelete(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSetEntity.Remove(entity);

        return Task.CompletedTask;
    }
}