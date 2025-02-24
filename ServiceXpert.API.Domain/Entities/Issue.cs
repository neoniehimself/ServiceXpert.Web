namespace ServiceXpert.API.Domain.Entities
{
    public class Issue : EntityBase
    {
        public int IssueID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
