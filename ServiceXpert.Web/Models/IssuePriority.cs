namespace ServiceXpert.Web.Models
{
    public class IssuePriority : ModelBase
    {
        public int IssuePriorityID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
