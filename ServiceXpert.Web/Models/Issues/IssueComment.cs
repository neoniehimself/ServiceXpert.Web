using ServiceXpert.Web.Models.Security;

namespace ServiceXpert.Web.Models.Issues;
public class IssueComment : AuditableModelBase<Guid>
{
    public string Content { get; set; } = string.Empty;

    public string IssueKey { get; set; } = string.Empty;

    public int IssueId { get; set; }

    public SecurityUser? CreatedByUser { get; set; }
}
