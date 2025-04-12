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

        public async Task<(IEnumerable<Issue>, Pagination)> GetPagedAllByStatusAsync(
            string statusCategory, int pageNumber, int pageSize, IncludeOptions<Issue>? includeOptions = null)
        {
            var (issues, paginationMetadata) = (Enumerable.Empty<Issue>(), new Pagination());

            if (Enum.TryParse(statusCategory, ignoreCase: true, out SxpEnums.IssueStatusCategory statusCategoryEnum))
            {
                switch (statusCategoryEnum)
                {
                    case SxpEnums.IssueStatusCategory.All:
                        (issues, paginationMetadata) = await this.issueRepository.GetPagedAllAsync(
                            pageNumber, pageSize, includeOptions: includeOptions);
                        break;
                    case SxpEnums.IssueStatusCategory.Open:
                        (issues, paginationMetadata) = await this.issueRepository.GetPagedAllAsync(
                            pageNumber, pageSize, i => (i.IssueStatusId != (int)SxpEnums.IssueStatus.Resolved) && (i.IssueStatusId != (int)SxpEnums.IssueStatus.Closed), includeOptions);
                        break;
                    case SxpEnums.IssueStatusCategory.Resolved:
                        (issues, paginationMetadata) = await this.issueRepository.GetPagedAllAsync(
                            pageNumber, pageSize, i => i.IssueStatusId == (int)SxpEnums.IssueStatus.Resolved, includeOptions);
                        break;
                    case SxpEnums.IssueStatusCategory.Closed:
                        (issues, paginationMetadata) = await this.issueRepository.GetPagedAllAsync(
                            pageNumber, pageSize, i => i.IssueStatusId == (int)SxpEnums.IssueStatus.Closed, includeOptions);
                        break;
                }
                return (issues, paginationMetadata);
            }

            throw new InvalidCastException($"Cannot cast string into enum. Value: {statusCategory}");
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
            try
            {
                if (int.TryParse(issueKey.Split('-')[1], out int issueID))
                {
                    return issueID;
                }
            }
            catch (IndexOutOfRangeException e)
            {
                throw new IndexOutOfRangeException("Failed to extract Id from Key", e);
            }

            return 0;
        }
    }
}
