namespace ServiceXpert.API.Application.DataTransferObjects
{
    public class IssuePriorityResponse : DataObjectBase
    {
        public int IssuePriorityID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
