namespace ServiceXpert.Web.Models.Issue;
public class Issue : ModelBase<int>
{
    public string IssueKey { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int IssueStatusId { get; set; }

    public IssueStatus? IssueStatus { get; set; }

    public int IssuePriorityId { get; set; }

    public IssuePriority? IssuePriority { get; set; }

    public ICollection<Comment.Comment> Comments { get; set; }

    public Guid? ReporterId { get; set; }

    public AspNetUserProfile.AspNetUserProfile? Reporter { get; set; }

    public Guid? AssigneeId { get; set; }

    public AspNetUserProfile.AspNetUserProfile? Assignee { get; set; }

    public AspNetUserProfile.AspNetUserProfile? CreatedByUser { get; set; }

    public AspNetUserProfile.AspNetUserProfile? ModifiedByUser { get; set; }

    public Issue()
    {
        this.Comments = [];
    }
}
