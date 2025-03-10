using System.ComponentModel.DataAnnotations;
using Enums = ServiceXpert.API.Domain.Shared.Enums;

namespace ServiceXpert.API.Application.DataTransferObjects.Issues
{
    public class IssueForCreate : DataObjectBase
    {
        [Required]
        [MaxLength(256)]
        public required string Name { get; set; }

        [MaxLength(4096)]
        public string? Description { get; set; }

        public int IssueStatusID { get; } = (int)Enums.Issue.IssueStatus.New;

        public int IssuePriorityID { get; set; } = (int)Enums.Issue.IssuePriority.Low;
    }
}
