namespace ServiceXpert.Web.Models;
// Do not inherit ModelBase
public class Pagination
{
    public int TotalCount { get; set; }

    public int TotalPageCount { get; set; }

    public int PageSize { get; set; }

    public int CurrentPage { get; set; }

    public Pagination()
    {
    }

    public Pagination(int totalCount, int pageSize, int currentPage)
    {
        this.TotalCount = totalCount;
        this.PageSize = pageSize;
        this.CurrentPage = currentPage;
        this.TotalPageCount = (int)Math.Ceiling(totalCount / (double)pageSize);
    }
}
