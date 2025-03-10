using AutoMapper;
using ServiceXpert.Api.Application.DataTransferObjects.Issues;
using Entities = ServiceXpert.Api.Domain.Entities;

namespace ServiceXpert.Api.Application.MapperProfiles
{
    class IssueStatusProfile : Profile
    {
        public IssueStatusProfile()
        {
            CreateMap<Entities.IssueStatus, IssueStatus>();
        }
    }
}
