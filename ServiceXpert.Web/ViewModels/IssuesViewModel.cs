using ServiceXpert.Web.Models;

namespace ServiceXpert.Web.ViewModels
{
    public class IssuesViewModel
    {
        public List<Issue> Issues { get; set; }

        public IssuesViewModel()
        {
            this.Issues = [];
        }
    }
}
