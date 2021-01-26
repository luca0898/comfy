using Comfy.Product.Contracts.Repositories.Shared;
using Comfy.Product.Contracts.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
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

        public async Task<IEnumerable<TEntity>> FindAll(CancellationToken cancellationToken, int skip = 0, int take = 20)
        {
            return await Task.Run(() =>
            {
                return _dbSetEntity
                    .AsQueryable()
                    .Where(schedule => schedule.Deleted == false)
                    .OrderBy(schedule => schedule.Id)
                    .Skip(skip)
                    .Take(take);

            }, cancellationToken);
        }

        public async Task<TEntity> FindOne(int id, CancellationToken cancellationToken)
        {
            return await _dbSetEntity.FindAsync(id, cancellationToken);
        }

        public async Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken)
        {
            EntityEntry<TEntity> newly = await _dbSetEntity.AddAsync(entity, cancellationToken);

            return newly.Entity;
        }
        public async Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                EntityEntry<TEntity> updatedEntity = _dbSetEntity.Update(entity);

                return updatedEntity.Entity;
            });
        }
        public async Task SoftDelete(TEntity entity, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                entity.Deleted = true;
                _dbSetEntity.Update(entity);

            }, cancellationToken);
        }
        public async Task HardDelete(TEntity entity, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                _dbSetEntity.Remove(entity);

            }, cancellationToken);
        }
    }
}
