using AutoMapper;
using ServiceXpert.API.Application.Abstractions.Interfaces.Services;
using ServiceXpert.API.Domain.Abstractions.Interfaces.Repositories;
using ServiceXpert.API.Domain.Entities;

namespace ServiceXpert.API.Application.Abstractions.Concretes.Services
{
    public class IssueStatusService : ServiceBase<int, IssueStatusResponse, IssueStatus>, IIssueStatusService
    {
        public IssueStatusService(IMapper mapper, IIssueStatusRepository repository) : base(mapper, repository)
        {
        }
    }
}
