using Microsoft.EntityFrameworkCore;
using PropLoader;
using ServiceXpert.API.Domain.Abstractions.Interfaces.Repositories;
using ServiceXpert.API.Domain.Entities;
using ServiceXpert.API.Infrastructure.DbContexts;

namespace ServiceXpert.API.Infrastructure.Abstractions.Concretes.Repositories
{
    public abstract class RepositoryBase<TEntityID, TEntity>
        : IRepositoryBase<TEntityID, TEntity>
        where TEntity : EntityBase
    {
        private SXPDbContext dbContext;

        protected RepositoryBase(SXPDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected string EntityID { get => string.Concat(typeof(TEntity).Name, "ID"); }

        public void Add(TEntity entity)
        {
            this.dbContext.Set<TEntity>().Add(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            await this.dbContext.Set<TEntity>().AddAsync(entity);
        }

        public void Attach(TEntity entity)
        {
            this.dbContext.Set<TEntity>().Attach(entity);
        }

        public void Delete(TEntity entity)
        {
            this.dbContext.Set<TEntity>().Remove(entity);
        }

        public void DeleteByID(TEntityID entityID)
        {
            this.dbContext.Set<TEntity>()
                .Where(e => EF.Property<TEntityID>(e, this.EntityID).Equals(entityID))
                .ExecuteDelete();
        }

        public async Task DeleteByIDAsync(TEntityID entityID)
        {
            await this.dbContext.Set<TEntity>()
                .Where(e => EF.Property<TEntityID>(e, this.EntityID).Equals(entityID))
                .ExecuteDeleteAsync();
        }

        public IEnumerable<TEntity> GetAll(IncludeOptions<TEntity>? includeOptions = null)
        {
            IQueryable<TEntity> query = OptionsBuilder.Build(this.dbContext.Set<TEntity>(), includeOptions);
            return query.ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(IncludeOptions<TEntity>? includeOptions = null)
        {
            IQueryable<TEntity> query = OptionsBuilder.Build(this.dbContext.Set<TEntity>(), includeOptions);
            return await query.ToListAsync();
        }

        public TEntity GetByID(TEntityID entityID, IncludeOptions<TEntity>? includeOptions = null)
        {
            IQueryable<TEntity> query = OptionsBuilder.Build(this.dbContext.Set<TEntity>(), includeOptions);
            return query.SingleOrDefault(e => EF.Property<TEntityID>(e, this.EntityID).Equals(entityID));
        }

        public async Task<TEntity> GetByIDAsync(TEntityID entityID, IncludeOptions<TEntity>? includeOptions = null)
        {
            IQueryable<TEntity> query = OptionsBuilder.Build(this.dbContext.Set<TEntity>(), includeOptions);
            return await query.SingleOrDefaultAsync(e => EF.Property<TEntityID>(e, this.EntityID).Equals(entityID));
        }

        public bool IsExistsByID(TEntityID entityID)
        {
            return this.dbContext.Set<TEntity>().Any(e => EF.Property<TEntityID>(e, this.EntityID).Equals(entityID));
        }

        public async Task<bool> IsExistsByIDAsync(TEntityID entityID)
        {
            return await this.dbContext.Set<TEntity>().AnyAsync(e => EF.Property<TEntityID>(e, this.EntityID).Equals(entityID));
        }

        public int SaveChanges()
        {
            return this.dbContext.SaveChanges();
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
