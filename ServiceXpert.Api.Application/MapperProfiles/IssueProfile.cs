using AutoMapper;
using ServiceXpert.Api.Application.DataTransferObjects;
using ServiceXpert.Api.Domain.Entities;

namespace ServiceXpert.Api.Application.MapperProfiles
{
    public class IssueProfile : Profile
    {
        public IssueProfile()
        {
            CreateMap<Issue, IssueDataObject>().ReverseMap();
            CreateMap<IssueDataObjectForCreate, Issue>();
            CreateMap<IssueDataObjectForUpdate, Issue>().ReverseMap();
        }
    }
}
