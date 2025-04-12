namespace ServiceXpert.Web.ViewModels
{
    public class IssueViewModel
    {
        public List<string> StatusCategories { get; set; }

        public IssueViewModel()
        {
            this.StatusCategories = [];
        }
    }
}
