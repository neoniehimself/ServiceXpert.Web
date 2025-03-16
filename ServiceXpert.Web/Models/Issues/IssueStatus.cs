namespace ServiceXpert.Web.Models.Issues
{
    public class IssueStatus : ModelBase
    {
        public int IssueStatusId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
