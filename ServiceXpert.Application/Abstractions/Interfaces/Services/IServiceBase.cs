using FluentBuilder.Core;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Shared;
using System.Linq.Expressions;

namespace ServiceXpert.Application.Abstractions.Interfaces.Services
{
    public interface IServiceBase<TEntityId, TEntity> where TEntity : EntityBase
    {
        Task<TEntity?> GetByIdAsync(TEntityId entityId, IncludeOptions<TEntity>? includeOptions = null);

        Task<(IEnumerable<TEntity>, Pagination)> GetPagedAllAsync(int pageNumber, int pageSize,
            Expression<Func<TEntity, bool>>? condition = null, IncludeOptions<TEntity>? includeOptions = null);

        Task<TEntityId> CreateAsync<TDataObject>(TDataObject dataObject) where TDataObject : DataObjectBase;

        Task UpdateByIdAsync<TDataObject>(TEntityId entityId, TDataObject dataObject) where TDataObject : DataObjectBase;

        Task DeleteByIdAsync(TEntityId entityId);

        Task<bool> IsExistsByIdAsync(TEntityId entityId);
    }
}
