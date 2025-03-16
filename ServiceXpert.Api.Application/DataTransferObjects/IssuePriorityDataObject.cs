namespace ServiceXpert.Api.Application.DataTransferObjects
{
    public class IssuePriorityDataObject : DataObjectBase
    {
        public int IssuePriorityID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
