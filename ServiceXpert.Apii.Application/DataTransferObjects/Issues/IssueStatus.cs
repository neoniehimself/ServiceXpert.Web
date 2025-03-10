namespace ServiceXpert.Api.Application.DataTransferObjects.Issues
{
    public class IssueStatus : DataObjectBase
    {
        public int IssueStatusID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
