using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PropLoader;
using ServiceXpert.API.Application.Abstractions.Interfaces.Services;
using ServiceXpert.API.Application.DataTransferObjects;
using ServiceXpert.API.Domain.Abstractions.Interfaces.Repositories;
using ServiceXpert.API.Domain.Entities;

namespace ServiceXpert.API.Application.Abstractions.Concretes.Services
{
    public abstract class ServiceBase<TID, TDataObject, TEntity>
        : IServiceBase<TID, TDataObject, TEntity>
        where TDataObject : DataObjectBase
        where TEntity : EntityBase
    {
        private readonly IMapper mapper;
        private readonly IRepositoryBase<TID, TEntity> repositoryBase;

        protected ServiceBase(IMapper mapper, IRepositoryBase<TID, TEntity> repositoryBase)
        {
            this.mapper = mapper;
            this.repositoryBase = repositoryBase;
        }

        private TID GetIDValue(TEntity entity)
        {
            var propID = typeof(TEntity).GetProperty($"{typeof(TEntity).Name}ID");

            var propIDValue = propID != null ? propID.GetValue(entity) : throw new NullReferenceException(nameof(propID));

            return propIDValue != null
                && propID.GetValue(entity) != null
                ? (TID)propID.GetValue(entity)! : throw new NullReferenceException($"{typeof(TEntity).Name}ID is null.");
        }

        public async Task<TID> AddAsync<TDataObjectForCreate>(TDataObjectForCreate dataObjectForCreate)
        {
            TEntity entity = this.mapper.Map<TEntity>(dataObjectForCreate);

            await this.repositoryBase.AddAsync(entity);
            await this.repositoryBase.SaveChangesAsync();

            return GetIDValue(entity);
        }

        public async Task<(TDataObjectForUpdate, ModelStateDictionary)> ConfigureForUpdateAsync<TDataObjectForUpdate>(TID id, JsonPatchDocument<TDataObjectForUpdate> patchDocument, ModelStateDictionary modelState) where TDataObjectForUpdate : DataObjectBase
        {
            TEntity? entity = await this.repositoryBase.GetByIDAsync(id);
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

        public async Task Delete(TDataObject dataObject)
        {
            TEntity entity = this.mapper.Map<TEntity>(dataObject);
            this.repositoryBase.Delete(entity);
            await this.repositoryBase.SaveChangesAsync();
        }

        public async Task DeleteByIDAsync(TID id)
        {
            await this.repositoryBase.DeleteByIDAsync(id);
            await this.repositoryBase.SaveChangesAsync();
        }

        public async Task<IEnumerable<TDataObject>> GetAllAsync(IncludeOptions<TEntity>? includeOptions = null)
        {
            IEnumerable<TEntity> entities = await this.repositoryBase.GetAllAsync(includeOptions);
            return this.mapper.Map<IEnumerable<TDataObject>>(entities);
        }

        public async Task<TDataObject?> GetByIDAsync(TID id, IncludeOptions<TEntity>? includeOptions = null)
        {
            TEntity entity = await this.repositoryBase.GetByIDAsync(id, includeOptions);
            return entity != null ? this.mapper.Map<TDataObject>(entity) : null;
        }

        public async Task<bool> IsExistsByIDAsync(TID id)
        {
            return await this.repositoryBase.IsExistsByIDAsync(id);
        }

        public async Task UpdateByIDAsync(TID id, TDataObject dataObject)
        {
            TEntity entity = await this.repositoryBase.GetByIDAsync(id);

            if (entity != null)
            {
                this.mapper.Map(dataObject, entity);
                this.repositoryBase.Update(entity);
                await this.repositoryBase.SaveChangesAsync();
            }
        }
    }
}
