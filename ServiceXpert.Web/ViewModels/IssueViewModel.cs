namespace ServiceXpert.Web.ViewModels;
public class IssueViewModel
{
    public ICollection<string> StatusCategories { get; set; }

    public IssueViewModel()
    {
        this.StatusCategories = [];
    }
}
