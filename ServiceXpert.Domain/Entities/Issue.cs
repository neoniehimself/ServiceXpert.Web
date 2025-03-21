using ServiceXpert.Domain.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceXpert.Domain.Entities
{
    public partial class Issue : EntityBase
    {
        public int IssueId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int IssueStatusId { get; set; }

        public IssueStatus? IssueStatus { get; set; }

        public int IssuePriorityId { get; set; }

        public IssuePriority? IssuePriority { get; set; }
    }

    public partial class Issue
    {
        [NotMapped]
        public string IssueKey => string.Concat(nameof(IssuePreFix.SXP), '-', this.IssueId);
    }
}
