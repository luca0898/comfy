using Comfy.Product.Contracts.Repositories.Shared;
using Comfy.Product.Contracts.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Comfy.Db.SQL.Repositories.Shared
{
    public abstract class GenericRelationalRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSetEntity;

        protected GenericRelationalRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSetEntity = _dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> FindAll(CancellationToken cancellationToken = default, int skip = 0, int take = 20)
        {
            return await _dbSetEntity
                .AsQueryable()
                .Where(schedule => schedule.Deleted == false)
                .OrderBy(schedule => schedule.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);
        }

        public async Task<TEntity> FindOne(int id, CancellationToken cancellationToken = default)
        {
            return await _dbSetEntity.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken = default)
        {
            EntityEntry<TEntity> newly = await _dbSetEntity.AddAsync(entity, cancellationToken);

            return newly.Entity;
        }
        public async Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                EntityEntry<TEntity> updatedEntity = _dbSetEntity.Update(entity);

                return updatedEntity.Entity;
            });
        }
        public async Task SoftDelete(TEntity entity, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                entity.Deleted = true;
                _dbSetEntity.Update(entity);

            }, cancellationToken);
        }
        public async Task HardDelete(TEntity entity, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                _dbSetEntity.Remove(entity);

            }, cancellationToken);
        }
    }
}
