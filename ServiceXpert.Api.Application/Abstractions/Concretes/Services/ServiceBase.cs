using AutoMapper;
using FluentBuilder.Core;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServiceXpert.Api.Application.Abstractions.Interfaces.Services;
using ServiceXpert.Api.Application.DataTransferObjects;
using ServiceXpert.Api.Domain.Abstractions.Interfaces.Repositories;
using ServiceXpert.Api.Domain.Entities;
using System.Linq.Expressions;

namespace ServiceXpert.Api.Application.Abstractions.Concretes.Services
{
    public abstract class ServiceBase<TId, TDataObject, TEntity>
        : IServiceBase<TId, TDataObject, TEntity>
        where TDataObject : DataObjectBase
        where TEntity : EntityBase
    {
        private readonly IMapper mapper;
        private readonly IRepositoryBase<TId, TEntity> repositoryBase;

        protected ServiceBase(IMapper mapper, IRepositoryBase<TId, TEntity> repositoryBase)
        {
            this.mapper = mapper;
            this.repositoryBase = repositoryBase;
        }

        private TId GetId(TEntity entity)
        {
            var propId = typeof(TEntity).GetProperty($"{typeof(TEntity).Name}Id");

            var propIdValue = propId != null ? propId.GetValue(entity) : throw new NullReferenceException(nameof(propId));

            return propIdValue != null
                && propId.GetValue(entity) != null
                ? (TId)propId.GetValue(entity)! : throw new NullReferenceException($"{typeof(TEntity).Name}Id is null.");
        }

        public async Task<TId> CreateAsync<TDataObjectForCreate>(TDataObjectForCreate dataObjectForCreate)
        {
            TEntity entity = this.mapper.Map<TEntity>(dataObjectForCreate);

            await this.repositoryBase.CreateAsync(entity);
            await this.repositoryBase.SaveChangesAsync();

            return GetId(entity);
        }

        private Expression<Func<TEntity, bool>> GetLambdaForId(TId id)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, string.Concat(typeof(TEntity).Name, "Id"));
            var constant = Expression.Constant(id);
            var equality = Expression.Equal(property, Expression.Convert(constant, property.Type));

            return Expression.Lambda<Func<TEntity, bool>>(equality, parameter);
        }

        public async Task<(TDataObjectForUpdate, ModelStateDictionary)> ConfigureForUpdateAsync<TDataObjectForUpdate>(
            TId id,
            JsonPatchDocument<TDataObjectForUpdate> patchDocument,
            ModelStateDictionary modelState)
            where TDataObjectForUpdate : DataObjectBase
        {

            TEntity? entity = await this.repositoryBase.GetAsync(GetLambdaForId(id));
            TDataObjectForUpdate? patchObject = default;

            if (entity != null)
            {
                try
                {
                    patchObject = this.mapper.Map<TDataObjectForUpdate>(entity);
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

        public async Task DeleteByIdAsync(TId id)
        {
            await this.repositoryBase.DeleteByIdAsync(id);
            await this.repositoryBase.SaveChangesAsync();
        }

        public async Task<IEnumerable<TDataObject>> GetAllAsync(IncludeOptions<TEntity>? includeOptions = null)
        {
            IEnumerable<TEntity> entities = await this.repositoryBase.GetAllAsync(includeOptions: includeOptions);
            return this.mapper.Map<IEnumerable<TDataObject>>(entities);
        }

        public async Task<TDataObject?> GetByIdAsync(TId id, IncludeOptions<TEntity>? includeOptions = null)
        {
            TEntity? entity = await this.repositoryBase.GetAsync(GetLambdaForId(id), includeOptions);
            return entity != null ? this.mapper.Map<TDataObject>(entity) : null;
        }

        public async Task<bool> IsExistsByIdAsync(TId id)
        {
            return await this.repositoryBase.IsExistsByIdAsync(id);
        }

        public async Task UpdateByIdAsync(TId id, TDataObject dataObject)
        {
            TEntity? entity = await this.repositoryBase.GetAsync(GetLambdaForId(id));

            if (entity != null)
            {
                this.mapper.Map(dataObject, entity);
                this.repositoryBase.Update(entity);
                await this.repositoryBase.SaveChangesAsync();
            }
        }
    }
}
