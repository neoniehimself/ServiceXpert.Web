using AutoMapper;
using FluentBuilder.Core;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServiceXpert.Application.Abstractions.Interfaces.Services;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Domain.Abstractions.Repositories;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Shared;
using System.Linq.Expressions;

namespace ServiceXpert.Application.Abstractions.Concretes.Services
{
    public abstract class ServiceBase<TEntityId, TEntity>(
        IRepositoryBase<TEntityId, TEntity> repositoryBase,
        IMapper mapper)
        : IServiceBase<TEntityId, TEntity> where TEntity : EntityBase
    {
        private readonly IRepositoryBase<TEntityId, TEntity> repositoryBase = repositoryBase;
        private readonly IMapper mapper = mapper;

        public async Task<TEntity?> GetByIdAsync(TEntityId entityId, IncludeOptions<TEntity>? includeOptions = null)
        {
            TEntity? entity = await this.repositoryBase.GetByIdAsync(entityId, includeOptions);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? condition = null, IncludeOptions<TEntity>? includeOptions = null)
        {
            return await this.repositoryBase.GetAllAsync(condition, includeOptions);
        }

        public async Task<(IEnumerable<TEntity>, PaginationMetadata)> GetPagedAllAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>>? condition = null, IncludeOptions<TEntity>? includeOptions = null)
        {
            return await this.repositoryBase.GetPagedAllAsync(pageNumber, pageSize, condition, includeOptions);
        }

        public async Task<TEntityId> CreateAsync<TDataObject>(TDataObject dataObject)
        {
            TEntity entity = this.mapper.Map<TEntity>(dataObject);

            await this.repositoryBase.CreateAsync(entity);
            await this.repositoryBase.SaveChangesAsync();

            return GetId(entity);
        }

        public async Task<(TDataObject, ModelStateDictionary)> ConfigureForUpdateAsync<TDataObject>(
            TEntityId entityId, JsonPatchDocument<TDataObject> patchDocument, ModelStateDictionary modelState)
            where TDataObject : DataObjectBase
        {
            TEntity? entity = await this.repositoryBase.GetByIdAsync(entityId);
            TDataObject? patchObject = default;

            if (entity != null)
            {
                try
                {
                    patchObject = this.mapper.Map<TDataObject>(entity);
                    patchDocument.ApplyTo(patchObject, modelState);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                throw new NullReferenceException($"{typeof(TEntity).Name} is null. Variable name: {nameof(entity)}");
            }

            return (patchObject, modelState);
        }

        public async Task UpdateAsync(TEntityId entityId, TEntity entity)
        {
            TEntity? entityToUpdate = await this.repositoryBase.GetByIdAsync(entityId);

            if (entityToUpdate != null)
            {
                this.repositoryBase.Attach(entityToUpdate);
                this.mapper.Map(entity, entityToUpdate);
                await this.repositoryBase.SaveChangesAsync();
            }
        }

        public async Task DeleteByIdAsync(TEntityId entityId)
        {
            await this.repositoryBase.DeleteByIdAsync(entityId);
            await this.repositoryBase.SaveChangesAsync();
        }

        public async Task<bool> IsExistsByIdAsync(TEntityId id)
        {
            return await this.repositoryBase.IsExistsByIdAsync(id);
        }

        private TEntityId GetId(TEntity entity)
        {
            var propId = typeof(TEntity).GetProperty($"{typeof(TEntity).Name}Id");

            var propIdValue = propId != null ? propId.GetValue(entity) : throw new NullReferenceException(nameof(propId));

            return propIdValue != null && propId.GetValue(entity) != null
                ? (TEntityId)propId.GetValue(entity)!
                : throw new NullReferenceException($"{typeof(TEntity).Name}Id is null.");
        }
    }
}
