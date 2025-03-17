namespace ServiceXpert.Web.ViewModels
{
    public class CreateIssueViewModel
    {
        public Dictionary<int, string> IssuePriorities { get; set; }

        public CreateIssueViewModel()
        {
            this.IssuePriorities = [];
        }
    }
}
