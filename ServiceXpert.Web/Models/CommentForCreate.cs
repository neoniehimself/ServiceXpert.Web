using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Web.Models;
public class CommentForCreate : ModelBase
{
    [Required]
    [MaxLength]
    public required string Content { get; set; } = string.Empty;

    public required int IssueId { get; set; }
}
