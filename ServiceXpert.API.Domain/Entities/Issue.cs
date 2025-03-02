using Enums = ServiceXpert.API.Domain.Shared.Enums;

namespace ServiceXpert.API.Domain.Entities
{
    public class Issue : EntityBase
    {
        public int IssueID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public Enums.Issue.IssueStatus IssueStatusID { get; set; }

        public IssueStatus? IssueStatus { get; set; }

        public Enums.Issue.IssuePriority IssuePriorityID { get; set; }

        public IssuePriority? IssuePriority { get; set; }
    }
}
