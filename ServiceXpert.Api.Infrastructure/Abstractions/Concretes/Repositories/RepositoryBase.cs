using Microsoft.EntityFrameworkCore;
using PropLoader;
using ServiceXpert.Api.Domain.Abstractions.Interfaces.Repositories;
using ServiceXpert.Api.Domain.Entities;
using ServiceXpert.Api.Infrastructure.DbContexts;

namespace ServiceXpert.Api.Infrastructure.Abstractions.Concretes.Repositories
{
    public abstract class RepositoryBase<TEntityID, TEntity>
        : IRepositoryBase<TEntityID, TEntity>
        where TEntity : EntityBase
    {
        private SxpDbContext dbContext;

        protected RepositoryBase(SxpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected string EntityID { get => string.Concat(typeof(TEntity).Name, "ID"); }

        public async Task AddAsync(TEntity entity)
        {
            await this.dbContext.Set<TEntity>().AddAsync(entity);
        }

        public void Attach(TEntity entity)
        {
            this.dbContext.Set<TEntity>().Attach(entity);
        }

        public async Task DeleteByIDAsync(TEntityID entityID)
        {
            await this.dbContext.Set<TEntity>()
                .Where(e => EF.Property<TEntityID>(e, this.EntityID)!.Equals(entityID))
                .ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(IncludeOptions<TEntity>? includeOptions = null)
        {
            IQueryable<TEntity> query = OptionsBuilder.Build(this.dbContext.Set<TEntity>(), includeOptions);
            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetByIDAsync(TEntityID entityID, IncludeOptions<TEntity>? includeOptions = null)
        {
            IQueryable<TEntity> query = OptionsBuilder.Build(this.dbContext.Set<TEntity>(), includeOptions);
            return await query.SingleOrDefaultAsync(e => EF.Property<TEntityID>(e, this.EntityID)!.Equals(entityID));
        }

        public async Task<bool> IsExistsByIDAsync(TEntityID entityID)
        {
            return await this.dbContext.Set<TEntity>().AnyAsync(e => EF.Property<TEntityID>(e, this.EntityID)!.Equals(entityID));
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.dbContext.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            this.dbContext.Set<TEntity>().Update(entity);
        }
    }
}
