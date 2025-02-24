using AutoMapper;
using PropLoader;
using ServiceXpert.API.Application.Abstractions.Interfaces.Services;
using ServiceXpert.API.Application.DataTransferObjects;
using ServiceXpert.API.Domain.Abstractions.Interfaces.Repositories;
using ServiceXpert.API.Domain.Entities;

namespace ServiceXpert.API.Application.Abstractions.Concretes.Services
{
    public class IssueService : ServiceBase<int, IssueResponse, Issue>, IIssueService
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
            if (int.TryParse(issueKey.Split('-')[1], out int issueID))
            {
                await this.issueRepository.DeleteByIDAsync(issueID);
            }
        }

        public async Task<IssueResponse?> GetByIDAsync(string issueKey, IncludeOptions<Issue>? includeOptions = null)
        {
            IssueResponse? issueResponse = null;

            if (int.TryParse(issueKey.Split('-')[1], out int issueID))
            {
                var issue = await this.issueRepository.GetByIDAsync(issueID, includeOptions);
                if (issue != null)
                {
                    issueResponse = this.mapper.Map<IssueResponse>(issue);
                }
            }

            return issueResponse;
        }
    }
}
