using AutoMapper;
using ServiceXpert.API.Application.DataTransferObjects;
using ServiceXpert.API.Domain.Entities;

namespace ServiceXpert.API.Application.MapperProfiles
{
    public class IssuePriorityProfile : Profile
    {
        public IssuePriorityProfile()
        {
            CreateMap<IssuePriority, IssuePriorityResponse>();
        }
    }
}
