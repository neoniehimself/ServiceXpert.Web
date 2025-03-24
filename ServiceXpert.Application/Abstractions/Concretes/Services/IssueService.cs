using AutoMapper;
using FluentBuilder.Core;
using ServiceXpert.Application.Abstractions.Interfaces.Services;
using ServiceXpert.Domain.Abstractions.Repositories;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Shared;
using SxpEnums = ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Application.Abstractions.Concretes.Services
{
    public class IssueService(
        IIssueRepository issueRepository,
        IMapper mapper)
        : ServiceBase<int, Issue>(issueRepository, mapper), IIssueService
    {
        private readonly IIssueRepository issueRepository = issueRepository;
        private readonly IMapper mapper = mapper;

        public async Task<Issue?> GetByIssueKey(string issueKey, IncludeOptions<Issue>? includeOptions = null)
        {
            var issueId = GetIdFromIssueKey(issueKey);
            return await this.issueRepository.GetByIdAsync(issueId, includeOptions);
        }

        public async Task<IEnumerable<Issue>> GetAllByStatusAsync(string status, IncludeOptions<Issue>? includeOptions = null)
        {
            var issues = Enumerable.Empty<Issue>();

            if (Enum.TryParse(status, ignoreCase: true, out SxpEnums.IssueStatus statusEnum))
            {
                switch (statusEnum)
                {
                    case SxpEnums.IssueStatus.Resolved:
                        issues = await this.issueRepository.GetAllAsync(i => i.IssueStatusId == (int)SxpEnums.IssueStatus.Resolved, includeOptions);
                        break;
                    case SxpEnums.IssueStatus.Closed:
                        issues = await this.issueRepository.GetAllAsync(i => i.IssueStatusId == (int)SxpEnums.IssueStatus.Closed, includeOptions);
                        break;
                    default:
                        goto Return;
                }
            }
            else
            {
                if (string.Equals(status, "All", StringComparison.OrdinalIgnoreCase))
                {
                    issues = await this.issueRepository.GetAllAsync(includeOptions: includeOptions);
                }
                else if (string.Equals(status, "Open", StringComparison.OrdinalIgnoreCase))
                {
                    issues = await this.issueRepository.GetAllAsync(
                        i => (i.IssueStatusId != (int)SxpEnums.IssueStatus.Resolved) && (i.IssueStatusId != (int)SxpEnums.IssueStatus.Closed),
                        includeOptions);
                }
            }
        Return:
            return issues;
        }

        public async Task<(IEnumerable<Issue>, PaginationMetadata)> GetPagedAllByStatusAsync(
            string status, int pageNumber, int pageSize, IncludeOptions<Issue>? includeOptions = null)
        {
            var (issues, paginationMetadata) = (Enumerable.Empty<Issue>(), new PaginationMetadata());

            if (Enum.TryParse(status, ignoreCase: true, out SxpEnums.IssueStatus statusEnum))
            {
                switch (statusEnum)
                {
                    case SxpEnums.IssueStatus.Resolved:
                        (issues, paginationMetadata) = await this.issueRepository.GetPagedAllAsync(
                            pageNumber,
                            pageSize,
                            i => i.IssueStatusId == (int)SxpEnums.IssueStatus.Resolved,
                            includeOptions);
                        break;
                    case SxpEnums.IssueStatus.Closed:
                        (issues, paginationMetadata) = await this.issueRepository.GetPagedAllAsync(
                            pageNumber,
                            pageSize,
                            i => i.IssueStatusId == (int)SxpEnums.IssueStatus.Closed,
                            includeOptions);
                        break;
                    default:
                        return (issues, paginationMetadata);
                }
            }
            else
            {
                if (string.Equals(status, "All", StringComparison.OrdinalIgnoreCase))
                {
                    (issues, paginationMetadata) = await this.issueRepository.GetPagedAllAsync(
                            pageNumber, pageSize, includeOptions: includeOptions);
                }
                else if (string.Equals(status, "Open", StringComparison.OrdinalIgnoreCase))
                {
                    (issues, paginationMetadata) = await this.issueRepository.GetPagedAllAsync(
                        pageNumber,
                        pageSize,
                        i => (i.IssueStatusId != (int)SxpEnums.IssueStatus.Resolved) && (i.IssueStatusId != (int)SxpEnums.IssueStatus.Closed),
                        includeOptions);
                }
            }
            return (issues, paginationMetadata);
        }

        public async Task UpdateByIssueKeyAsync(string issueKey, Issue issue)
        {
            var issueToUpdate = await this.issueRepository.GetByIdAsync(GetIdFromIssueKey(issueKey));

            if (issueToUpdate != null)
            {
                this.issueRepository.Attach(issueToUpdate);
                this.mapper.Map(issue, issueToUpdate);
                await this.issueRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteByIssueKeyAsync(string issueKey)
        {
            await this.issueRepository.DeleteByIdAsync(GetIdFromIssueKey(issueKey));
        }

        public async Task<bool> IsExistsByIssueKeyAsync(string issueKey)
        {
            return await this.issueRepository.IsExistsByIdAsync(GetIdFromIssueKey(issueKey));
        }

        public int GetIdFromIssueKey(string issueKey)
        {
            if (int.TryParse(issueKey.Split('-')[1], out int issueID))
            {
                return issueID;
            }

            throw new InvalidOperationException();
        }
    }
}
