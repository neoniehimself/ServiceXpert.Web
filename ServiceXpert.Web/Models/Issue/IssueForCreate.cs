using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Web.Models.Issue;
public class IssueForCreate : ModelBase
{
    [Required]
    [MaxLength(256)]
    public required string Name { get; set; }

    [MaxLength(4096)]
    public string? Description { get; set; }

    public int IssueStatusId { get; set; } = (int)Enums.IssueStatus.New;

    public int IssuePriorityId { get; set; } = (int)Enums.IssuePriority.Low;
}
