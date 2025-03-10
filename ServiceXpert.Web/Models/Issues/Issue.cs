namespace ServiceXpert.Web.Models.Issues
{
    public class Issue : ModelBase
    {
        public int IssueID { get; set; }

        public string IssueKey { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int IssueStatusID { get; set; }

        public IssueStatus? IssueStatus { get; set; }

        public int IssuePriorityID { get; set; }

        public IssuePriority? IssuePriority { get; set; }
    }
}
