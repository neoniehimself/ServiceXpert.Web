using ServiceXpert.API.Domain.Shared.Enums.Entity;

namespace ServiceXpert.API.Application.DataTransferObjects
{
    public class IssueResponse : DataObjectBase
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
    }

    public class IssueForCreateRequest : DataObjectBase
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }

    public class IssueForUpdateRequest : DataObjectBase
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
