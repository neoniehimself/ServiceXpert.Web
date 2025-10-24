using ServiceXpert.Web.Models.Security;

namespace ServiceXpert.Web.ViewModels;
public class CreateIssueViewModel
{
    public Dictionary<int, string> IssuePriorities { get; set; }

    public ICollection<SecurityProfile> Users { get; set; }

    public CreateIssueViewModel()
    {
        this.IssuePriorities = [];
        this.Users = [];
    }
}
