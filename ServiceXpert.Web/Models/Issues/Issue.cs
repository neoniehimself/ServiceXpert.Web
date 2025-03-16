using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Web.Models.Issues
{
    public class Issue : ModelBase
    {
        public int IssueId { get; set; }

        public string IssueKey { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int IssueStatusId { get; set; }

        public IssueStatus? IssueStatus { get; set; }

        public int IssuePriorityId { get; set; }

        public IssuePriority? IssuePriority { get; set; }
    }

    public class IssueForCreate : ModelBase
    {
        [Required]
        [MaxLength(256)]
        public required string Name { get; set; }

        [MaxLength(4096)]
        public string? Description { get; set; }

        public int IssuePriorityId { get; set; }
    }
}
