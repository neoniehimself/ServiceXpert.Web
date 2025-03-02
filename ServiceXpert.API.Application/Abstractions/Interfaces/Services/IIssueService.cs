using PropLoader;
using ServiceXpert.API.Application.DataTransferObjects;
using ServiceXpert.API.Domain.Entities;

namespace ServiceXpert.API.Application.Abstractions.Interfaces.Services
{
    public interface IIssueService : IServiceBase<int, IssueResponse, Issue>
    {
        Task<IssueResponse?> GetByIDAsync(string issueKey, IncludeOptions<Issue>? includeOptions = null);

        Task DeleteByIDAsync(string issueKey);

        Task<bool> IsExistsByIDAsync(string issueKey);

        int GetIssueID(string issueKey);

        Task UpdateByIDAsync(string issueKey, IssueForUpdateRequest issueForUpdateRequest);
    }
}
