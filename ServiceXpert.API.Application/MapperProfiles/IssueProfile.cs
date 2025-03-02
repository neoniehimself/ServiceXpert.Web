using AutoMapper;
using ServiceXpert.API.Application.DataTransferObjects;
using ServiceXpert.API.Domain.Entities;

namespace ServiceXpert.API.Application.MapperProfiles
{
    public class IssueProfile : Profile
    {
        public IssueProfile()
        {
            CreateMap<Issue, IssueResponse>().ReverseMap();
            CreateMap<IssueForCreateRequest, Issue>();
            CreateMap<IssueForUpdateRequest, Issue>().ReverseMap();
        }
    }
}
