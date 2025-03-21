using ServiceXpert.Domain.Entities;

namespace ServiceXpert.Web.ViewModels
{
    public class IssueViewModel
    {
        public List<Issue> Issues { get; set; }

        public IssueViewModel()
        {
            this.Issues = [];
        }
    }
}
