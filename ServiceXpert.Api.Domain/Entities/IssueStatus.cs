namespace ServiceXpert.Api.Domain.Entities
{
    public class IssueStatus : EntityBase
    {
        public int IssueStatusId { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
