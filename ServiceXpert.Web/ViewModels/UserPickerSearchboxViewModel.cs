using ServiceXpert.Web.Models.AspNetUserProfile;

namespace ServiceXpert.Web.ViewModels;
public class UserPickerSearchboxViewModel(string field, AspNetUserProfile? aspNetUserProfile = null)
{
    public string Label { get; } = field;

    public string HiddenInputName { get; } = string.Concat(field, "Id");

    public AspNetUserProfile? UserProfile { get; } = aspNetUserProfile;
}
