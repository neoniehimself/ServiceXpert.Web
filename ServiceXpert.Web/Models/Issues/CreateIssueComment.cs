using ServiceXpert.Web.Utils;
using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Web.Models.Issues;
public class CreateIssueComment : CreateModelBase
{
    [Required]
    public required string Content { get; set; } = string.Empty;

    [Required]
    public required string IssueKey { get; set; } = null!;

    public int IssueId { get => IssueUtil.GetIdFromKey(this.IssueKey); }
}
