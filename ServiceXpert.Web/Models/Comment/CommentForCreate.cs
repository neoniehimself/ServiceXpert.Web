using ServiceXpert.Web.Utils;
using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Web.Models.Comment;
public class CommentForCreate : ModelBase
{
    [Required]
    [MaxLength]
    public required string Content { get; set; } = string.Empty;

    [Required]
    [MaxLength(7)] // SXP-999
    public required string IssueKey { get; set; }

    public int IssueId { get => IssueUtil.GetIdFromIssueKey(this.IssueKey); }
}
