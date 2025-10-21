namespace ServiceXpert.Web.ViewModels.Issues;
public class CreateIssueViewModel
{
    public Dictionary<int, string> IssuePriorities { get; set; }

    public CreateIssueViewModel()
    {
        this.IssuePriorities = [];
    }
}
