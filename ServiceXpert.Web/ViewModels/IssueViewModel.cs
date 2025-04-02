namespace ServiceXpert.Web.ViewModels
{
    public class IssueViewModel
    {
        public List<string> NavigationTabs { get; set; }

        public List<string> TableHeaders { get; set; }

        public IssueViewModel()
        {
            this.NavigationTabs = [];
            this.TableHeaders = [];
        }
    }
}
