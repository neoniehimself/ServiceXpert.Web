namespace ServiceXpert.Web.Models;
public class PagedResult<T>
{
    public List<T> Items { get; }

    public Pagination Pagination { get; }

    public PagedResult()
    {
        this.Items = [];
        this.Pagination = new();
    }
}
