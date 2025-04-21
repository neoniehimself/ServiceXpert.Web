using System.ComponentModel.DataAnnotations;
using DomainEnums = ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Application.DataObjects
{
    public class IssueDataObjectForCreate : DataObjectBase
    {
        [Required]
        [MaxLength(256)]
        public required string Name { get; set; }

        [MaxLength(4096)]
        public string? Description { get; set; }

        public int IssueStatusId { get; } = (int)DomainEnums.IssueStatus.New;

        public int IssuePriorityId { get; set; } = (int)DomainEnums.IssuePriority.Low;
    }

    public class IssueDataObjectForUpdate : DataObjectBase
    {
        [Required]
        public required string IssueKey { get; set; }

        public int IssueId
        {
            get
            {
                return int.TryParse(this.IssueKey.Split('-')[1], out int issueId) ? issueId : throw new IndexOutOfRangeException();
            }
        }

        [Required]
        [MaxLength(256)]
        public required string Name { get; set; }

        [MaxLength(4096)]
        public string? Description { get; set; }

        public required int IssueStatusId { get; set; }

        public required int IssuePriorityId { get; set; }
    }
}
