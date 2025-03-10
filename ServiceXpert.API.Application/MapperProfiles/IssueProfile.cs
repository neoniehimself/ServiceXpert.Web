using AutoMapper;
using ServiceXpert.API.Application.DataTransferObjects.Issues;
using Entities = ServiceXpert.API.Domain.Entities;

namespace ServiceXpert.API.Application.MapperProfiles
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
