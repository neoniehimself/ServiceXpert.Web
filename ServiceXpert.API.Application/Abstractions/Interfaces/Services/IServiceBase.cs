using PropLoader;
using ServiceXpert.API.Application.DataTransferObjects;
using ServiceXpert.API.Domain.Entities;

namespace ServiceXpert.API.Application.Abstractions.Interfaces.Services
{
    public interface IServiceBase<TDataObject, TEntity, TID>
        where TDataObject : DataObjectBase
        where TEntity : EntityBase
    {
        TDataObject GetByID(TID id, IncludeOptions<TEntity>? includeOptions = null);

        Task<TDataObject> GetByIDAsync(TID id, IncludeOptions<TEntity>? includeOptions = null);

        IEnumerable<TDataObject> GetAll(IncludeOptions<TEntity>? includeOptions = null);

        Task<IEnumerable<TDataObject>> GetAllAsync(IncludeOptions<TEntity>? includeOptions = null);

        TID Add(TDataObject dataObject);

        Task<TID> AddAsync(TDataObject dataObject);

        void AddRange(params TDataObject[] entities);

        Task AddRangeAsync(params TDataObject[] entities);

        void AddRange(List<TDataObject> entities);

        Task AddRangeAsync(List<TDataObject> entities);

        void UpdateByID(TID id, TDataObject dataObject);

        Task UpdateByIDAsync(TID id, TDataObject dataObject);

        void DeleteByID(TID id);

        Task DeleteByIDAsync(TID id);

        void Delete(TDataObject dataObject);

        bool IsExistsByID(TID id);

        Task<bool> IsExistsByIDAsync(TID id);
    }
}
