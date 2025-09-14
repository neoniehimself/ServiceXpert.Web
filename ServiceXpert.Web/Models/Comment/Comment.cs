namespace ServiceXpert.Web.Models.Comment;
public class Comment : ModelBase<Guid>
{
    public string Content { get; set; } = string.Empty;

    public string IssueKey { get; set; } = string.Empty;

    public int IssueId { get; set; }

    public AspNetUserProfile.AspNetUserProfile? CreatedByUser { get; set; }
}
