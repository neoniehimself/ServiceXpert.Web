namespace ServiceXpert.Domain.Entities
{
    public class IssueStatus : EntityBase
    {
        public int IssueStatusId { get; set; }

        public string Name { get; set; } = string.Empty;

        public int IssueStatusCategoryId { get; set; }

        public IssueStatusCategory? IssueStatusCategory { get; set; }
    }
}
