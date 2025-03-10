using AutoMapper;
using ServiceXpert.API.Application.DataTransferObjects.Issues;
using Entities = ServiceXpert.API.Domain.Entities;

namespace ServiceXpert.API.Application.MapperProfiles
{
    public class IssuePriorityProfile : Profile
    {
        public IssuePriorityProfile()
        {
            CreateMap<Entities.IssuePriority, IssuePriority>();
        }
    }
}
