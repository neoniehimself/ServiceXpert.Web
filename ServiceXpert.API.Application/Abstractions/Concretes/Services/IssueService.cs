using AutoMapper;
using PropLoader;
using ServiceXpert.API.Application.Abstractions.Interfaces.Services;
using ServiceXpert.API.Application.DataTransferObjects.Issues;
using ServiceXpert.API.Domain.Abstractions.Interfaces.Repositories;
using Entities = ServiceXpert.API.Domain.Entities;
using Enums = ServiceXpert.API.Domain.Shared.Enums;

namespace ServiceXpert.API.Application.Abstractions.Concretes.Services
{
    public class IssueService : ServiceBase<int, Issue, Entities.Issue>, IIssueService
    {
        private readonly IMapper mapper;
        private readonly IIssueRepository issueRepository;

        public IssueService(IMapper mapper, IIssueRepository issueRepository) : base(mapper, issueRepository)
        {
            this.mapper = mapper;
            this.issueRepository = issueRepository;
        }

        public async Task DeleteByIDAsync(string issueKey)
        {
            await this.issueRepository.DeleteByIDAsync(GetIssueID(issueKey));
        }

        public async Task<Issue?> GetByIDAsync(string issueKey, IncludeOptions<Entities.Issue>? includeOptions = null)
        {
            Issue? issueResponse = null;

            var issue = await this.issueRepository.GetByIDAsync(GetIssueID(issueKey), includeOptions);
            if (issue != null)
            {
                issueResponse = this.mapper.Map<Issue>(issue);
            }

            return issueResponse;
        }

        public int GetIssueID(string issueKey)
        {
            if (int.TryParse(issueKey.Split('-')[1], out int issueID))
            {
                return issueID;
            }

            throw new InvalidOperationException();
        }

        public IEnumerable<string> GetIssuePriorities()
        {
            var issuePriorities = new List<string>();

            foreach (var issuePriority in Enum.GetValues(typeof(Enums.Issue.IssuePriority)))
            {
                issuePriorities.Add(issuePriority.ToString()!);
            }

            return issuePriorities;
        }

        public async Task<bool> IsExistsByIDAsync(string issueKey)
        {
            return await this.issueRepository.IsExistsByIDAsync(GetIssueID(issueKey));
        }

        public async Task UpdateByIDAsync(string issueKey, IssueForUpdate issueForUpdateRequest)
        {
            var issue = await this.issueRepository.GetByIDAsync(GetIssueID(issueKey));

            if (issue != null)
            {
                this.mapper.Map(issueForUpdateRequest, issue);
                this.issueRepository.Update(issue);
                await this.issueRepository.SaveChangesAsync();
            }
        }
    }
}
