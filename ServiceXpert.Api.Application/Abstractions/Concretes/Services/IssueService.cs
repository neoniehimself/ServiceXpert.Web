using AutoMapper;
using FluentBuilder.Core;
using ServiceXpert.Api.Application.Abstractions.Interfaces.Services;
using ServiceXpert.Api.Application.DataTransferObjects;
using ServiceXpert.Api.Domain.Abstractions.Interfaces.Repositories;
using ServiceXpert.Api.Domain.Entities;

namespace ServiceXpert.Api.Application.Abstractions.Concretes.Services
{
    public class IssueService : ServiceBase<int, IssueDataObject, Issue>, IIssueService
    {
        private readonly IMapper mapper;
        private readonly IIssueRepository issueRepository;

        public IssueService(IMapper mapper, IIssueRepository issueRepository) : base(mapper, issueRepository)
        {
            this.mapper = mapper;
            this.issueRepository = issueRepository;
        }

        public async Task<IssueDataObject?> GetByIdAsync(string issueKey, IncludeOptions<Issue>? includeOptions = null)
        {
            IssueDataObject? issueResponse = null;

            var issueId = GetIdFromKey(issueKey);
            var issue = await this.issueRepository.GetAsync(issueId, includeOptions);

            if (issue != null)
            {
                issueResponse = this.mapper.Map<IssueDataObject>(issue);
            }

            return issueResponse;
        }

        public async Task UpdateByIdAsync(string issueKey, IssueDataObjectForUpdate dataObject)
        {
            var issueId = GetIdFromKey(issueKey);

            var issue = await this.issueRepository.GetAsync(i => i.IssueId == issueId);

            if (issue != null)
            {
                this.issueRepository.Attach(issue); // Attach to track the changes
                this.mapper.Map(dataObject, issue);
                this.issueRepository.Update(issue);
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
