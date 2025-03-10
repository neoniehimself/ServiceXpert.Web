namespace ServiceXpert.Api.Application.DataTransferObjects.Issues
{
    public class IssuePriority : DataObjectBase
    {
        public int IssuePriorityID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
