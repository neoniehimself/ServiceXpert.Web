namespace ServiceXpert.Web.Models;
// Do not inherit ModelBase
public class PagedResult<T>
{
    public ICollection<T> Items { get; }

    public Pagination Pagination { get; }

    public PagedResult()
    {
        this.Items = [];
        this.Pagination = new();
    }
}
