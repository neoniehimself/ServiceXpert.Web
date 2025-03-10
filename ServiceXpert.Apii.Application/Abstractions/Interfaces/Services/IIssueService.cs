using PropLoader;
using ServiceXpert.Api.Application.DataTransferObjects.Issues;
using Entities = ServiceXpert.Api.Domain.Entities;

namespace ServiceXpert.Api.Application.Abstractions.Interfaces.Services
{
    public interface IIssueService : IServiceBase<int, Issue, Entities.Issue>
    {
        Task<Issue?> GetByIDAsync(string issueKey, IncludeOptions<Entities.Issue>? includeOptions = null);

        Task DeleteByIDAsync(string issueKey);

        Task<bool> IsExistsByIDAsync(string issueKey);

        int GetIssueID(string issueKey);

        Task UpdateByIDAsync(string issueKey, IssueForUpdate issueForUpdateRequest);

        IEnumerable<string> GetIssuePriorities();
    }
}
