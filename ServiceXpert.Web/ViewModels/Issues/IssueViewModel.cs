namespace ServiceXpert.Web.ViewModels.Issues;
public class IssueViewModel
{
    public ICollection<string> StatusCategories { get; set; }

    public IssueViewModel()
    {
        this.StatusCategories = [];
    }
}
