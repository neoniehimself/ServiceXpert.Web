namespace ServiceXpert.Web.Models;
public class PagedIssuesResponse
{
    public List<Issue> Issues { get; set; }

    public Pagination Pagination { get; set; }

    public PagedIssuesResponse()
    {
        this.Issues = [];
        this.Pagination = new();
    }
}
