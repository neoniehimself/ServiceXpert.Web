using ServiceXpert.Domain.Entities;

namespace ServiceXpert.Web.ViewModels
{
    public class IssueDetailsViewModel(Issue issue, bool isEdit = false)
    {
        public Issue Issue { get; set; } = issue;

        public bool IsEdit { get; set; } = isEdit;
    }
}
