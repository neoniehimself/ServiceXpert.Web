namespace ServiceXpert.Web.ValueObjects;
// Do not inherit ModelBase
public class PaginationResult<T>
{
    public ICollection<T> Items { get; }

    public Pagination Pagination { get; }

    public PaginationResult()
    {
        this.Items = [];
        this.Pagination = new();
    }
}
