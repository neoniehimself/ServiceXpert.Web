using ServiceXpert.API.Application.DataTransferObjects;
using Enums = ServiceXpert.API.Domain.Shared.Enums;

namespace ServiceXpert.API.Domain.Entities
{
    public class IssueStatusResponse : DataObjectBase
    {
        public Enums.Issue.IssueStatus IssueStatusID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
