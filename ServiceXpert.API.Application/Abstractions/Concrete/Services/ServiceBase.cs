using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PropLoader;
using ServiceXpert.API.Application.Abstractions.Interfaces.Services;
using ServiceXpert.API.Application.DataTransferObjects;
using ServiceXpert.API.Domain.Abstractions.Interfaces.Repositories;
using ServiceXpert.API.Domain.Entities;

namespace ServiceXpert.API.Application.Abstractions.Concrete.Services
{
    public abstract class ServiceBase<TDataObject, TEntity, TID>
        : IServiceBase<TDataObject, TEntity, TID>
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

        public TID Add(TDataObject dataObject)
        {
            throw new NotImplementedException();
        }

        public Task<TID> AddAsync(TDataObject dataObject)
        {
            throw new NotImplementedException();
        }

        public (TDataObject, ModelStateDictionary) ConfigureForUpdate(TID id, JsonPatchDocument<TDataObject> patchDocument, ModelStateDictionary modelState)
        {
            TEntity? entity = this.repositoryBase.GetByID(id);
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

        public async Task<(TDataObject, ModelStateDictionary)> ConfigureForUpdateAsync(TID id, JsonPatchDocument<TDataObject> patchDocument, ModelStateDictionary modelState)
        {
            TEntity? entity = await this.repositoryBase.GetByIDAsync(id);
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

        public void Delete(TDataObject dataObject)
        {
            TEntity entity = this.mapper.Map<TEntity>(dataObject);
            this.repositoryBase.Delete(entity);
            this.repositoryBase.SaveChanges();
        }

        public void DeleteByID(TID id)
        {
            this.repositoryBase.DeleteByID(id);
            this.repositoryBase.SaveChanges();
        }

        public async Task DeleteByIDAsync(TID id)
        {
            await this.repositoryBase.DeleteByIDAsync(id);
            await this.repositoryBase.SaveChangesAsync();
        }

        public IEnumerable<TDataObject> GetAll(IncludeOptions<TEntity>? includeOptions = null)
        {
            IEnumerable<TEntity> entities = this.repositoryBase.GetAll(includeOptions);
            return this.mapper.Map<IEnumerable<TDataObject>>(entities);
        }

        public async Task<IEnumerable<TDataObject>> GetAllAsync(IncludeOptions<TEntity>? includeOptions = null)
        {
            IEnumerable<TEntity> entities = await this.repositoryBase.GetAllAsync(includeOptions);
            return this.mapper.Map<IEnumerable<TDataObject>>(entities);
        }

        public TDataObject? GetByID(TID id, IncludeOptions<TEntity>? includeOptions = null)
        {
            TEntity entity = this.repositoryBase.GetByID(id, includeOptions);
            return entity != null ? this.mapper.Map<TDataObject>(entity) : null;
        }

        public async Task<TDataObject?> GetByIDAsync(TID id, IncludeOptions<TEntity>? includeOptions = null)
        {
            TEntity entity = await this.repositoryBase.GetByIDAsync(id, includeOptions);
            return entity != null ? this.mapper.Map<TDataObject>(entity) : null;
        }

        public bool IsExistsByID(TID id)
        {
            return this.repositoryBase.IsExistsByID(id);
        }

        public async Task<bool> IsExistsByIDAsync(TID id)
        {
            return await this.repositoryBase.IsExistsByIDAsync(id);
        }

        public void UpdateByID(TID id, TDataObject dataObject)
        {
            TEntity entity = this.repositoryBase.GetByID(id);

            if (entity != null)
            {
                this.mapper.Map(dataObject, entity);
                this.repositoryBase.Update(entity);
                this.repositoryBase.SaveChanges();
            }
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
