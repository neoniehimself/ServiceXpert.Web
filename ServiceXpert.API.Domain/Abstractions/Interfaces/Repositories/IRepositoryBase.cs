using PropLoader;
using ServiceXpert.API.Domain.Entities;

namespace ServiceXpert.API.Domain.Abstractions.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntityID, TEntity> where TEntity : EntityBase
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();

        void Attach(TEntity entity);

        TEntity GetByID(TEntityID entityID, IncludeOptions<TEntity>? includeOptions = null);

        Task<TEntity> GetByIDAsync(TEntityID entityID, IncludeOptions<TEntity>? includeOptions = null);

        IEnumerable<TEntity> GetAll(IncludeOptions<TEntity>? includeOptions = null);

        Task<IEnumerable<TEntity>> GetAllAsync(IncludeOptions<TEntity>? includeOptions = null);

        void Add(TEntity entity);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void DeleteByID(TEntityID entityID);

        Task DeleteByIDAsync(TEntityID entityID);

        void Delete(TEntity entity);

        bool IsExistsByID(TEntityID entityID);

        Task<bool> IsExistsByIDAsync(TEntityID entityID);
    }
}
