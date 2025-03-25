using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Shared;

namespace ServiceXpert.Web.ViewModels
{
    public class IssueListPageViewModel
    {
        public List<Issue> Issues { get; set; }

        public PaginationMetadata Metadata { get; set; }

        public IssueListPageViewModel()
        {
            this.Issues = [];
            this.Metadata = new();
        }
    }
}
