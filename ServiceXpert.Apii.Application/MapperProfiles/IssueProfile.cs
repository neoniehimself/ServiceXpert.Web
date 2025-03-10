using AutoMapper;
using ServiceXpert.Api.Application.DataTransferObjects.Issues;
using Entities = ServiceXpert.Api.Domain.Entities;

namespace ServiceXpert.Api.Application.MapperProfiles
{
    public class IssueProfile : Profile
    {
        public IssueProfile()
        {
            CreateMap<Entities.Issue, Issue>().ReverseMap();
            CreateMap<IssueForCreate, Entities.Issue>();
            CreateMap<IssueForUpdate, Entities.Issue>().ReverseMap();
        }
    }
}
