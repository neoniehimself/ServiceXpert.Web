using Enums = ServiceXpert.Api.Domain.Shared.Enums;

namespace ServiceXpert.Api.Application.DataTransferObjects.Issues
{
    public class Issue : DataObjectBase
    {
        public int IssueID { get; set; }

        public string IssueKey
        {
            get
            {
                return string.Concat(nameof(Enums.Issue.IssuePreFix.SXP), '-', this.IssueID);
            }
        }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int IssueStatusID { get; set; }

        public IssueStatus? IssueStatus { get; set; }

        public int IssuePriorityID { get; set; }

        public IssuePriority? IssuePriority { get; set; }
    }
}
