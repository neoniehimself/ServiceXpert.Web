using AutoMapper;
using ServiceXpert.Api.Application.DataTransferObjects;
using ServiceXpert.Api.Domain.Entities;

namespace ServiceXpert.Api.Application.MapperProfiles
{
    public class IssuePriorityProfile : Profile
    {
        public IssuePriorityProfile()
        {
            CreateMap<IssuePriority, IssuePriorityDataObject>();
        }
    }
}
