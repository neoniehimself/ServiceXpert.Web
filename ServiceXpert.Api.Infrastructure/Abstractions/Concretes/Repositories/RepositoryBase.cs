using FluentBuilder.Core;
using FluentBuilder.Persistence;
using Microsoft.EntityFrameworkCore;
using ServiceXpert.Api.Domain.Abstractions.Interfaces.Repositories;
using ServiceXpert.Api.Domain.Entities;
using ServiceXpert.Api.Infrastructure.DbContexts;
using System.Linq.Expressions;

namespace ServiceXpert.Api.Infrastructure.Abstractions.Concretes.Repositories
{
    public abstract class RepositoryBase<TEntityId, TEntity>
        : IRepositoryBase<TEntityId, TEntity>
        where TEntity : EntityBase
    {
        private SxpDbContext dbContext;

        protected RepositoryBase(SxpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected string EntityId { get => string.Concat(typeof(TEntity).Name, "Id"); }

        public async Task<int> SaveChangesAsync()
        {
            return await this.dbContext.SaveChangesAsync();
        }

        public void Attach(TEntity entity)
        {
            this.dbContext.Set<TEntity>().Attach(entity);
        }

        public async Task<TEntity?> GetAsync(TEntityId entityId, IncludeOptions<TEntity>? includeOptions = null)
        {
            IQueryable<TEntity> query = QueryBuilder.Build(this.dbContext.Set<TEntity>(), includeOptions);
            return await query.SingleOrDefaultAsync(e => EF.Property<TEntityId>(e, this.EntityId)!.Equals(entityId));
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> condition, IncludeOptions<TEntity>? includeOptions = null)
        {
            IQueryable<TEntity> query = QueryBuilder.Build(this.dbContext.Set<TEntity>(), includeOptions);
            return await query.SingleOrDefaultAsync(condition);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? condition = null, IncludeOptions<TEntity>? includeOptions = null)
        {
            IQueryable<TEntity> query = QueryBuilder.Build(this.dbContext.Set<TEntity>(), includeOptions);

            if (condition != null)
            {
                query = query.Where(condition);
            }

            return await query.ToListAsync();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await this.dbContext.Set<TEntity>().AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            this.dbContext.Set<TEntity>().Update(entity);
        }

        public async Task DeleteByIdAsync(TEntityId entityId)
        {
            await this.dbContext.Set<TEntity>().Where(e => EF.Property<TEntityId>(e, this.EntityId)!.Equals(entityId)).ExecuteDeleteAsync();
        }

        public async Task<bool> IsExistsByIdAsync(TEntityId entityId)
        {
            return await this.dbContext.Set<TEntity>().AnyAsync(e => EF.Property<TEntityId>(e, this.EntityId)!.Equals(entityId));
        }
    }
}
