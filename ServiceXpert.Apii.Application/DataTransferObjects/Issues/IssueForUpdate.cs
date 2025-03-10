using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Api.Application.DataTransferObjects.Issues
{
    public class IssueForUpdate : DataObjectBase
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
