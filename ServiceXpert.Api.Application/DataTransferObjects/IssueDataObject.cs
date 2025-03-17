using ServiceXpert.Api.Domain.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using SharedEnums = ServiceXpert.Shared.Enums;

namespace ServiceXpert.Api.Application.DataTransferObjects
{
    public class IssueDataObject : DataObjectBase
    {
        public int IssueID { get; set; }

        public string IssueKey
        {
            get
            {
                return string.Concat(nameof(IssuePreFix.SXP), '-', this.IssueID);
            }
        }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int IssueStatusID { get; set; }

        public IssueStatusDataObject? IssueStatus { get; set; }

        public int IssuePriorityID { get; set; }

        public IssuePriorityDataObject? IssuePriority { get; set; }
    }

    public class IssueDataObjectForCreate : DataObjectBase
    {
        [Required]
        [MaxLength(256)]
        public required string Name { get; set; }

        [MaxLength(4096)]
        public string? Description { get; set; }

        public int IssueStatusID { get; } = (int)SharedEnums.IssueStatus.New;

        public int IssuePriorityID { get; set; } = (int)SharedEnums.IssuePriority.Low;
    }

    public class IssueDataObjectForUpdate : DataObjectBase
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
