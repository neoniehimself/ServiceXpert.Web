using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Shared;

namespace ServiceXpert.Web.ViewModels
{
    public class IssueViewModel
    {
        public List<Issue> Issues { get; set; }

        public PaginationMetadata Metadata { get; set; }

        public IssueViewModel()
        {
            this.Issues = [];
            this.Metadata = new();
        }
    }
}
