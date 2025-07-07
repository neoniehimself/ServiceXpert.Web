namespace ServiceXpert.Web.Models.Comment;
public class Comment : ModelBase
{
    public Guid CommentId { get; set; }

    public string Content { get; set; } = string.Empty;

    public string IssueKey { get; set; } = string.Empty;

    public int IssueId { get; set; }
}
