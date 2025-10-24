using ServiceXpert.Web.Models.Security;

namespace ServiceXpert.Web.Models.Issues;
public class Issue : AuditableModelBase<int>
{
    public string IssueKey { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int IssueStatusId { get; set; }

    public IssueStatus? IssueStatus { get; set; }

    public int IssuePriorityId { get; set; }

    public IssuePriority? IssuePriority { get; set; }

    public ICollection<IssueComment> Comments { get; set; }

    public Guid? ReporterId { get; set; }

    public SecurityUser? Reporter { get; set; }

    public Guid? AssigneeId { get; set; }

    public SecurityUser? Assignee { get; set; }

    public SecurityUser? CreatedByUser { get; set; }

    public SecurityUser? ModifiedByUser { get; set; }

    public Issue()
    {
        this.Comments = [];
    }
}
