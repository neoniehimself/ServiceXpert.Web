namespace ServiceXpert.Api.Domain.Entities
{
    public class Issue : EntityBase
    {
        public int IssueId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int IssueStatusId { get; set; }

        public IssueStatus? IssueStatus { get; set; }

        public int IssuePriorityId { get; set; }

        public IssuePriority? IssuePriority { get; set; }
    }
}
