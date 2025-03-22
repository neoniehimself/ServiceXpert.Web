using FluentBuilder.Core;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Shared;

namespace ServiceXpert.Application.Abstractions.Interfaces.Services
{
    public interface IIssueService : IServiceBase<int, Issue>
    {
        Task<Issue?> GetByIdAsync(string issueKey, IncludeOptions<Issue>? includeOptions = null);

        Task<IEnumerable<Issue>> GetAllAsync(string status, IncludeOptions<Issue>? includeOptions = null);

        Task<(IEnumerable<Issue>, PaginationMetadata)> GetPagedAllAsync(
            string status, int pageNumber, int pageSize, IncludeOptions<Issue>? includeOptions = null);

        Task UpdateByIdAsync(string issueKey, Issue issue);

        Task DeleteByIdAsync(string issueKey);

        Task<bool> IsExistsByIdAsync(string issueKey);

        int GetIdFromKey(string issueKey);
    }
}
