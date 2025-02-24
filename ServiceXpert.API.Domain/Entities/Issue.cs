using Enums = ServiceXpert.API.Domain.Shared.Enums;

namespace ServiceXpert.API.Domain.Entities
{
    public class Issue : EntityBase
    {
        public int IssueID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public Enums.Issue.IssueStatus IssueStatusID { get; set; } = Enums.Issue.IssueStatus.New;

        public IssueStatus IssueStatus { get; set; } = null!;
    }
}
