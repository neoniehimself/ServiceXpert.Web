using ServiceXpert.Domain.Entities;

namespace ServiceXpert.Web.ViewModels
{
    public class EditIssueViewModel
    {
        public Issue Issue { get; set; }

        public Dictionary<int, string> IssuePriorities { get; set; }

        public Dictionary<int, string> IssueStatuses { get; set; }

        public EditIssueViewModel(Issue issue)
        {
            this.Issue = issue;
            this.IssuePriorities = [];
            this.IssueStatuses = [];
        }
    }
}
