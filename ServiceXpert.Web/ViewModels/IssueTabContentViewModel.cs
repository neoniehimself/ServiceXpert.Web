using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Shared;

namespace ServiceXpert.Web.ViewModels
{
    public class IssueTabContentViewModel
    {
        public List<Issue> Issues { get; set; }

        public Pagination PaginationMetadata { get; set; }

        public IssueTabContentViewModel()
        {
            this.Issues = [];
            this.PaginationMetadata = new();
        }
    }
}
