using AutoMapper;
using ServiceXpert.API.Domain.Entities;

namespace ServiceXpert.API.Application.MapperProfiles
{
    class IssueStatusProfile : Profile
    {
        public IssueStatusProfile()
        {
            CreateMap<IssueStatus, IssueStatusResponse>();
        }
    }
}
