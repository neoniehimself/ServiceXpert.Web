namespace ServiceXpert.Api.Application.DataTransferObjects
{
    public class IssueStatusDataObject : DataObjectBase
    {
        public int IssueStatusID { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
