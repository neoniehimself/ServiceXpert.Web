namespace ServiceXpert.Web.Models.Issues
{
    public class IssuePriority : ModelBase
    {
        public int IssuePriorityID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
