using AutoMapper;
using ServiceXpert.API.Application.DataTransferObjects.Issues;
using Entities = ServiceXpert.API.Domain.Entities;

namespace ServiceXpert.API.Application.MapperProfiles
{
    class IssueStatusProfile : Profile
    {
        public IssueStatusProfile()
        {
            CreateMap<Entities.IssueStatus, IssueStatus>();
        }
    }
}
