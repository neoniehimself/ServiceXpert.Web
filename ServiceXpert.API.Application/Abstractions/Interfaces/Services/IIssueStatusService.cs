using ServiceXpert.API.Domain.Entities;

namespace ServiceXpert.API.Application.Abstractions.Interfaces.Services
{
    public interface IIssueStatusService : IServiceBase<int, IssueStatusResponse, IssueStatus>
    {
    }
}
