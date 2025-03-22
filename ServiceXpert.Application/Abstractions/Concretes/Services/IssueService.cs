using AutoMapper;
using FluentBuilder.Core;
using ServiceXpert.Application.Abstractions.Interfaces.Services;
using ServiceXpert.Domain.Abstractions.Repositories;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Shared;
using SxpEnums = ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Application.Abstractions.Concretes.Services
{
    public class IssueService : ServiceBase<int, Issue>, IIssueService
    {
        private readonly IIssueRepository issueRepository;
        private readonly IMapper mapper;

        public IssueService(IIssueRepository issueRepository, IMapper mapper) : base(issueRepository, mapper)
        {
            this.issueRepository = issueRepository;
            this.mapper = mapper;
        }

        public async Task<Issue?> GetByIdAsync(string issueKey, IncludeOptions<Issue>? includeOptions = null)
        {
            var issueId = GetIdFromKey(issueKey);
            return await this.issueRepository.GetAsync(issueId, includeOptions);
        }

        public async Task<IEnumerable<Issue>> GetAllAsync(string status, IncludeOptions<Issue>? includeOptions = null)
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
                        i => (i.IssueStatusId != (int)SxpEnums.IssueStatus.Resolved) && (i.IssueStatusId != (int)SxpEnums.IssueStatus.Closed), includeOptions);
                }
            }
        Return:
            return issues;
        }

        public async Task<(IEnumerable<Issue>, PaginationMetadata)> GetPagedAllAsync(
            string status, int pageNumber, int pageSize, IncludeOptions<Issue>? includeOptions = null)
        {
            var issues = (Enumerable.Empty<Issue>(), new PaginationMetadata());

            if (Enum.TryParse(status, ignoreCase: true, out SxpEnums.IssueStatus statusEnum))
            {
                switch (statusEnum)
                {
                    case SxpEnums.IssueStatus.Resolved:
                        issues = await this.issueRepository.GetPagedAllAsync(
                            pageNumber, pageSize, i => i.IssueStatusId == (int)SxpEnums.IssueStatus.Resolved, includeOptions);
                        break;
                    case SxpEnums.IssueStatus.Closed:
                        issues = await this.issueRepository.GetPagedAllAsync(
                            pageNumber, pageSize, i => i.IssueStatusId == (int)SxpEnums.IssueStatus.Closed, includeOptions);
                        break;
                    default:
                        goto Return;
                }
            }
            else
            {
                if (string.Equals(status, "All", StringComparison.OrdinalIgnoreCase))
                {
                    issues = await this.issueRepository.GetPagedAllAsync(
                            pageNumber, pageSize, includeOptions: includeOptions);
                }
                else if (string.Equals(status, "Open", StringComparison.OrdinalIgnoreCase))
                {
                    issues = await this.issueRepository.GetPagedAllAsync(
                        pageNumber,
                        pageSize,
                        i => (i.IssueStatusId != (int)SxpEnums.IssueStatus.Resolved) && (i.IssueStatusId != (int)SxpEnums.IssueStatus.Closed),
                        includeOptions);
                }
            }
        Return:
            return issues;
        }

        public async Task UpdateByIdAsync(string issueKey, Issue issue)
        {
            var issueToUpdate = await this.issueRepository.GetAsync(GetIdFromKey(issueKey));

            if (issueToUpdate != null)
            {
                this.issueRepository.Attach(issueToUpdate); // Attach to track the changes
                this.mapper.Map(issue, issueToUpdate);
                this.issueRepository.Update(issueToUpdate);
                await this.issueRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteByIdAsync(string issueKey)
        {
            await this.issueRepository.DeleteByIdAsync(GetIdFromKey(issueKey));
        }

        public async Task<bool> IsExistsByIdAsync(string issueKey)
        {
            return await this.issueRepository.IsExistsByIdAsync(GetIdFromKey(issueKey));
        }

        public int GetIdFromKey(string issueKey)
        {
            if (int.TryParse(issueKey.Split('-')[1], out int issueID))
            {
                return issueID;
            }

            throw new InvalidOperationException();
        }
    }
}
