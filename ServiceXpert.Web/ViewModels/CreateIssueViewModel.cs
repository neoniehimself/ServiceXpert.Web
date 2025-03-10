namespace ServiceXpert.Web.ViewModels
{
    public class CreateIssueViewModel
    {
        public List<string> IssuePriorities { get; set; }

        public CreateIssueViewModel()
        {
            this.IssuePriorities = [];
        }
    }
}
