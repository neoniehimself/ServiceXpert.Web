using ServiceXpert.Web.Enums.Issues;

namespace ServiceXpert.Web.ViewModels.Issues;
public class SearchFormViewModel
{
    public string IssueKey { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string StatusCategory { get; set; } = IssueStatusCategory.All.ToString();
}
