using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Web.Models.Issue;
public class IssueForCreate : ModelBaseForCreate
{
    [Required]
    public required string Name { get; set; }

    public string? Description { get; set; }

    public int IssueStatusId { get; set; } = (int)Enums.IssueStatus.New;

    public int IssuePriorityId { get; set; } = (int)Enums.IssuePriority.Low;
}
