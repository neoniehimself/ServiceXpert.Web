using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Web.Models.Issues;
public class CreateIssue : CreateModelBase
{
    [Required(ErrorMessage = "The Title field is required.")]
    public required string Name { get; set; }

    public string? Description { get; set; }

    public int IssueStatusId { get; set; } = (int)Enums.Issues.IssueStatus.New;

    public int IssuePriorityId { get; set; } = (int)Enums.Issues.IssuePriority.Low;

    public Guid? ReporterId { get; set; }

    public Guid? AssigneeId { get; set; }
}
