namespace ServiceXpert.Web.Models;
public class IssuePriority : ModelBase
{
    public int IssuePriorityId { get; set; }

    public string Name { get; set; } = string.Empty;
}
