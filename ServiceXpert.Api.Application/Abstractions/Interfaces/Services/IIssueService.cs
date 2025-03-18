using FluentBuilder.Core;
using ServiceXpert.Api.Application.DataTransferObjects;
using ServiceXpert.Api.Domain.Entities;

namespace ServiceXpert.Api.Application.Abstractions.Interfaces.Services
{
    public interface IIssueService : IServiceBase<int, IssueDataObject, Issue>
    {
        Task<IssueDataObject?> GetByIdAsync(string issueKey, IncludeOptions<Issue>? includeOptions = null);

        Task<IEnumerable<IssueDataObject>> GetAllAsync(string status, IncludeOptions<Issue>? includeOptions = null);

        Task UpdateByIdAsync(string issueKey, IssueDataObjectForUpdate dataObject);

        Task DeleteByIdAsync(string issueKey);

        Task<bool> IsExistsByIdAsync(string issueKey);

        int GetIdFromKey(string issueKey);
    }
}
