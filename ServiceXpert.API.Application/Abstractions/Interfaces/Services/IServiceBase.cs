using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PropLoader;
using ServiceXpert.API.Application.DataTransferObjects;
using ServiceXpert.API.Domain.Entities;

namespace ServiceXpert.API.Application.Abstractions.Interfaces.Services
{
    public interface IServiceBase<TID, TDataObject, TEntity>
        where TDataObject : DataObjectBase
        where TEntity : EntityBase
    {
        TDataObject? GetByID(TID id, IncludeOptions<TEntity>? includeOptions = null);

        Task<TDataObject?> GetByIDAsync(TID id, IncludeOptions<TEntity>? includeOptions = null);

        IEnumerable<TDataObject> GetAll(IncludeOptions<TEntity>? includeOptions = null);

        Task<IEnumerable<TDataObject>> GetAllAsync(IncludeOptions<TEntity>? includeOptions = null);

        TID Add<TDataObjectForCreate>(TDataObjectForCreate dataObjectForCreate);

        Task<TID> AddAsync<TDataObjectForCreate>(TDataObjectForCreate dataObjectForCreate);

        (TDataObjectForUpdate, ModelStateDictionary) ConfigureForUpdate<TDataObjectForUpdate>(TID id, JsonPatchDocument<TDataObjectForUpdate> patchDocument, ModelStateDictionary modelState) where TDataObjectForUpdate : DataObjectBase;

        Task<(TDataObjectForUpdate, ModelStateDictionary)> ConfigureForUpdateAsync<TDataObjectForUpdate>(TID id, JsonPatchDocument<TDataObjectForUpdate> patchDocument, ModelStateDictionary modelState) where TDataObjectForUpdate : DataObjectBase;

        void UpdateByID(TID id, TDataObject dataObject);

        Task UpdateByIDAsync(TID id, TDataObject dataObject);

        void DeleteByID(TID id);

        Task DeleteByIDAsync(TID id);

        void Delete(TDataObject dataObject);

        bool IsExistsByID(TID id);

        Task<bool> IsExistsByIDAsync(TID id);
    }
}
