using AutoMapper;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Domain.Entities;

namespace ServiceXpert.Application.MapperProfiles
{
    public class IssueProfile : Profile
    {
        public IssueProfile()
        {
            CreateMap<IssueDataObjectForCreate, Issue>();
            CreateMap<IssueDataObjectForUpdate, Issue>()
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore());
        }
    }
}
