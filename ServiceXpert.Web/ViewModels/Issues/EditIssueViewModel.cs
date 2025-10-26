using ServiceXpert.Web.Models.Issues;

namespace ServiceXpert.Web.ViewModels.Issues;
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
