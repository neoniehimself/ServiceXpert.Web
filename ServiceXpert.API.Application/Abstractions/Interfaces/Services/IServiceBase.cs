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
        Task<TDataObject?> GetByIDAsync(TID id, IncludeOptions<TEntity>? includeOptions = null);

        Task<IEnumerable<TDataObject>> GetAllAsync(IncludeOptions<TEntity>? includeOptions = null);

        Task<TID> AddAsync<TDataObjectForCreate>(TDataObjectForCreate dataObjectForCreate);

        Task<(TDataObjectForUpdate, ModelStateDictionary)> ConfigureForUpdateAsync<TDataObjectForUpdate>(TID id, JsonPatchDocument<TDataObjectForUpdate> patchDocument, ModelStateDictionary modelState) where TDataObjectForUpdate : DataObjectBase;

        Task UpdateByIDAsync(TID id, TDataObject dataObject);

        Task DeleteByIDAsync(TID id);

        Task Delete(TDataObject dataObject);

        Task<bool> IsExistsByIDAsync(TID id);
    }
}
