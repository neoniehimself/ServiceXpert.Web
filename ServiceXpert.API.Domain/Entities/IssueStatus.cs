namespace ServiceXpert.Api.Domain.Entities
{
    public class IssueStatus : EntityBase
    {
        public int IssueStatusID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
