using ServiceXpert.API.Application.DataTransferObjects;
using ServiceXpert.API.Domain.Entities;

namespace ServiceXpert.API.Application.Abstractions.Interfaces.Services
{
    public interface IIssueService : IServiceBase<int, IssueResponse, Issue>
    {
    }
}
