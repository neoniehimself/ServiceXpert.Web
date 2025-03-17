using AutoMapper;
using ServiceXpert.Api.Application.DataTransferObjects;
using ServiceXpert.Api.Domain.Entities;

namespace ServiceXpert.Api.Application.MapperProfiles
{
    class IssueStatusProfile : Profile
    {
        public IssueStatusProfile()
        {
            CreateMap<IssueStatus, IssueStatusDataObject>();
        }
    }
}
