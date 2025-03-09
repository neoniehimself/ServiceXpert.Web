using ServiceXpert.API.Application.DataTransferObjects;

namespace ServiceXpert.API.Domain.Entities
{
    public class IssueStatusResponse : DataObjectBase
    {
        public int IssueStatusID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
