using ServiceXpert.Web.Models.AspNetUserProfile;

namespace ServiceXpert.Web.ViewModels;
public class CreateIssueViewModel
{
    public Dictionary<int, string> IssuePriorities { get; set; }

    public ICollection<AspNetUserProfile> Users { get; set; }

    public CreateIssueViewModel()
    {
        this.IssuePriorities = [];
        this.Users = [];
    }
}
