using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Web.Models.Issue;
public class IssueForUpdate : ModelBase
{
    [Required]
    [MaxLength(256)]
    public required string Name { get; set; }

    [MaxLength(4096)]
    public string? Description { get; set; }

    [Required]
    public required int IssueStatusId { get; set; }

    [Required]
    public required int IssuePriorityId { get; set; }
}
