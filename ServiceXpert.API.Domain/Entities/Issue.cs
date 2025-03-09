namespace ServiceXpert.API.Domain.Entities
{
    public class Issue : EntityBase
    {
        public int IssueID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int IssueStatusID { get; set; }

        public IssueStatus? IssueStatus { get; set; }

        public int IssuePriorityID { get; set; }

        public IssuePriority? IssuePriority { get; set; }
    }
}
