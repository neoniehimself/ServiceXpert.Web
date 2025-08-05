using ServiceXpert.Web.Models.Issue;

namespace ServiceXpert.Web.ViewModels;

public class EditIssueViewModel
{
    public required Issue Issue { get; set; }

    public Dictionary<int, string> IssuePriorities { get; set; }

    public Dictionary<int, string> IssueStatuses { get; set; }

    public EditIssueViewModel()
    {
        this.IssuePriorities = [];
        this.IssueStatuses = [];
    }
}
