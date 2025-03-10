using AutoMapper;
using ServiceXpert.Api.Application.DataTransferObjects.Issues;
using Entities = ServiceXpert.Api.Domain.Entities;

namespace ServiceXpert.Api.Application.MapperProfiles
{
    public class IssuePriorityProfile : Profile
    {
        public IssuePriorityProfile()
        {
            CreateMap<Entities.IssuePriority, IssuePriority>();
        }
    }
}
