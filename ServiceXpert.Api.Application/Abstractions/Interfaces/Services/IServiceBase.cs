using FluentBuilder.Core;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServiceXpert.Api.Application.DataTransferObjects;
using ServiceXpert.Api.Domain.Entities;
using System.Linq.Expressions;

namespace ServiceXpert.Api.Application.Abstractions.Interfaces.Services
{
    public interface IServiceBase<TId, TDataObject, TEntity>
        where TDataObject : DataObjectBase
        where TEntity : EntityBase
    {
        Task<TDataObject?> GetByIdAsync(TId id, IncludeOptions<TEntity>? includeOptions = null);

        Task<IEnumerable<TDataObject>> GetAllAsync(Expression<Func<TEntity, bool>>? condition = null, IncludeOptions<TEntity>? includeOptions = null);

        Task<TId> CreateAsync<TDataObjectForCreate>(TDataObjectForCreate dataObjectForCreate);

        Task<(TDataObjectForUpdate, ModelStateDictionary)> ConfigureForUpdateAsync<TDataObjectForUpdate>(
            TId id,
            JsonPatchDocument<TDataObjectForUpdate> patchDocument,
            ModelStateDictionary modelState)
            where TDataObjectForUpdate : DataObjectBase;

        Task UpdateByIdAsync(TId id, TDataObject dataObject);

        Task DeleteByIdAsync(TId id);

        Task<bool> IsExistsByIdAsync(TId id);
    }
}
