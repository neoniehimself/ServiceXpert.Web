using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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

        public TID Add(TDataObject dataObject)
        {
            throw new NotImplementedException();
        }

        public Task<TID> AddAsync(TDataObject dataObject)
        {
            throw new NotImplementedException();
        }

        public void AddRange(params TDataObject[] entities)
        {
            throw new NotImplementedException();
        }

        public void AddRange(List<TDataObject> entities)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(params TDataObject[] entities)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(List<TDataObject> entities)
        {
            throw new NotImplementedException();
        }

        public (TDataObject, ModelStateDictionary) ConfigureForUpdate(TID id, JsonPatchDocument patchDocument, ModelStateDictionary modelState)
        {
            throw new NotImplementedException();
        }

        public Task<(TDataObject, ModelStateDictionary)> ConfigureForUpdateAsync(TID id, JsonPatchDocument patchDocument, ModelStateDictionary modelState)
        {
            throw new NotImplementedException();
        }

        public void Delete(TDataObject dataObject)
        {
            throw new NotImplementedException();
        }

        public void DeleteByID(TID id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIDAsync(TID id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TDataObject> GetAll(IncludeOptions<TEntity>? includeOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TDataObject>> GetAllAsync(IncludeOptions<TEntity>? includeOptions = null)
        {
            throw new NotImplementedException();
        }

        public TDataObject GetByID(TID id, IncludeOptions<TEntity>? includeOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<TDataObject> GetByIDAsync(TID id, IncludeOptions<TEntity>? includeOptions = null)
        {
            throw new NotImplementedException();
        }

        public bool IsExistsByID(TID id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistsByIDAsync(TID id)
        {
            throw new NotImplementedException();
        }

        public void UpdateByID(TID id, TDataObject dataObject)
        {
            throw new NotImplementedException();
        }

        public Task UpdateByIDAsync(TID id, TDataObject dataObject)
        {
            throw new NotImplementedException();
        }
    }
}
