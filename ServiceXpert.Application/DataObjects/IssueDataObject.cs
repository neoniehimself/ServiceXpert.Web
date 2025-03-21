using System.ComponentModel.DataAnnotations;
using SxpEnums = ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Application.DataObjects
{
    public class IssueDataObjectForCreate : DataObjectBase
    {
        [Required]
        [MaxLength(256)]
        public required string Name { get; set; }

        [MaxLength(4096)]
        public string? Description { get; set; }

        public int IssueStatusId { get; } = (int)SxpEnums.IssueStatus.New;

        public int IssuePriorityId { get; set; } = (int)SxpEnums.IssuePriority.Low;
    }

    public class IssueDataObjectForUpdate : DataObjectBase
    {
        [Required]
        [MaxLength(256)]
        public required string Name { get; set; }

        [MaxLength(4096)]
        public string? Description { get; set; }

        public required int IssueStatusId { get; set; }

        public required int IssuePriorityId { get; set; }
    }
}
