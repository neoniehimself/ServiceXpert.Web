using PropLoader;
using ServiceXpert.Api.Domain.Entities;

namespace ServiceXpert.Api.Domain.Abstractions.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntityID, TEntity> where TEntity : EntityBase
    {
        Task<int> SaveChangesAsync();

        void Attach(TEntity entity);

        Task<TEntity?> GetByIDAsync(TEntityID entityID, IncludeOptions<TEntity>? includeOptions = null);

        Task<IEnumerable<TEntity>> GetAllAsync(IncludeOptions<TEntity>? includeOptions = null);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        Task DeleteByIDAsync(TEntityID entityID);

        Task<bool> IsExistsByIDAsync(TEntityID entityID);
    }
}
