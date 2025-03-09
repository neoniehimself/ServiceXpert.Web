using ServiceXpert.API.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using Enums = ServiceXpert.API.Domain.Shared.Enums;

namespace ServiceXpert.API.Application.DataTransferObjects
{
    public class IssueResponse : DataObjectBase
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

        public IssueStatusResponse? IssueStatus { get; set; }

        public int IssuePriorityID { get; set; }

        public IssuePriorityResponse? IssuePriority { get; set; }
    }

    public class IssueForCreateRequest : DataObjectBase
    {
        [Required]
        [MaxLength(256)]
        public required string Name { get; set; }

        [MaxLength(4096)]
        public string? Description { get; set; }

        public int IssueStatusID { get; } = (int)Enums.Issue.IssueStatus.New;

        public int IssuePriorityID { get; set; } = (int)Enums.Issue.IssuePriority.Low;
    }

    public class IssueForUpdateRequest : DataObjectBase
    {
        [Required]
        [MaxLength(256)]
        public required string Name { get; set; }

        [MaxLength(4096)]
        public string? Description { get; set; }

        public required int IssueStatusID { get; set; }

        public required int IssuePriorityID { get; set; }
    }
}
