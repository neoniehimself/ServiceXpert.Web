using FluentBuilder.Core;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Shared;
using System.Linq.Expressions;

namespace ServiceXpert.Application.Abstractions.Interfaces.Services
{
    public interface IServiceBase<TEntityId, TEntity> where TEntity : EntityBase
    {
        Task<TEntity?> GetByIdAsync(TEntityId entityId, IncludeOptions<TEntity>? includeOptions = null);

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? condition = null, IncludeOptions<TEntity>? includeOptions = null);

        Task<(IEnumerable<TEntity>, PaginationMetadata)> GetPagedAllAsync(
            int pageNumber, int pageSize, Expression<Func<TEntity, bool>>? condition = null, IncludeOptions<TEntity>? includeOptions = null);

        Task<TEntityId> CreateAsync<TDataObject>(TDataObject dataObject);

        Task<(TDataObject, ModelStateDictionary)> ConfigureForUpdateAsync<TDataObject>(
            TEntityId entityId, JsonPatchDocument<TDataObject> patchDocument, ModelStateDictionary modelState)
            where TDataObject : DataObjectBase;

        Task UpdateAsync(TEntityId entityId, TEntity entity);

        Task DeleteByIdAsync(TEntityId entityId);

        Task<bool> IsExistsByIdAsync(TEntityId entityId);
    }
}
