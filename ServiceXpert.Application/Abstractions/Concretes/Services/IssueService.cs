using AutoMapper;
using FluentBuilder.Core;
using ServiceXpert.Application.Abstractions.Interfaces.Services;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Domain.Abstractions.Repositories;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Shared;
using DomainEnums = ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Application.Abstractions.Concretes.Services
{
    public class IssueService : ServiceBase<int, Issue>, IIssueService
    {
        private readonly IIssueRepository issueRepository;
        private readonly IMapper mapper;

        public IssueService(
            IIssueRepository issueRepository,
            IMapper mapper)
            : base(issueRepository, mapper)
        {
            this.issueRepository = issueRepository;
            this.mapper = mapper;
        }

        public async Task<Issue?> GetByIssueKey(string issueKey, IncludeOptions<Issue>? includeOptions = null)
        {
            var issueId = GetIdFromIssueKey(issueKey);
            return await this.issueRepository.GetByIdAsync(issueId, includeOptions);
        }

        public async Task<(IEnumerable<Issue>, Pagination)> GetPagedAllByStatusAsync(
            string statusCategory, int pageNumber, int pageSize, IncludeOptions<Issue>? includeOptions = null)
        {
            var (issues, paginationMetadata) = (Enumerable.Empty<Issue>(), new Pagination());

            if (Enum.TryParse(statusCategory, ignoreCase: true, out DomainEnums.IssueStatusCategory statusCategoryEnum))
            {
                switch (statusCategoryEnum)
                {
                    case DomainEnums.IssueStatusCategory.All:
                        (issues, paginationMetadata) = await this.issueRepository.GetPagedAllAsync(
                            pageNumber, pageSize, includeOptions: includeOptions);
                        break;
                    case DomainEnums.IssueStatusCategory.Open:
                        (issues, paginationMetadata) = await this.issueRepository.GetPagedAllAsync(
                            pageNumber, pageSize, i => (i.IssueStatusId != (int)DomainEnums.IssueStatus.Resolved) && (i.IssueStatusId != (int)DomainEnums.IssueStatus.Closed), includeOptions);
                        break;
                    case DomainEnums.IssueStatusCategory.Resolved:
                        (issues, paginationMetadata) = await this.issueRepository.GetPagedAllAsync(
                            pageNumber, pageSize, i => i.IssueStatusId == (int)DomainEnums.IssueStatus.Resolved, includeOptions);
                        break;
                    case DomainEnums.IssueStatusCategory.Closed:
                        (issues, paginationMetadata) = await this.issueRepository.GetPagedAllAsync(
                            pageNumber, pageSize, i => i.IssueStatusId == (int)DomainEnums.IssueStatus.Closed, includeOptions);
                        break;
                }
                return (issues, paginationMetadata);
            }

            throw new InvalidCastException($"Cannot cast string into enum. Value: {statusCategory}");
        }

        public async Task UpdateByIssueKeyAsync(string issueKey, IssueDataObjectForUpdate issue)
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
