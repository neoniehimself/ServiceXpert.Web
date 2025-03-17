namespace ServiceXpert.Web.Models
{
    public class IssueStatus : ModelBase
    {
        public int IssueStatusId { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
