using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Web.Models.Issues
{
    public class IssueForCreate : ModelBase
    {
        [Required]
        [MaxLength(256)]
        public required string Name { get; set; }

        [MaxLength(4096)]
        public string? Description { get; set; }

        public int IssuePriorityID { get; set; }
    }
}
