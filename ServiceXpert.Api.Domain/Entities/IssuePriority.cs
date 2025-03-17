namespace ServiceXpert.Api.Domain.Entities
{
    public class IssuePriority : EntityBase
    {
        public int IssuePriorityId { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
