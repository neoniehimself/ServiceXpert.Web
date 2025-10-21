using ServiceXpert.Web.Models.Security;

namespace ServiceXpert.Web.ViewModels;
public class UserPickerSearchboxViewModel(string field, SecurityProfile? aspNetUserProfile = null)
{
    public string Label { get; } = field;

    public string HiddenInputName { get; } = string.Concat(field, "Id");

    public SecurityProfile? UserProfile { get; } = aspNetUserProfile;
}
