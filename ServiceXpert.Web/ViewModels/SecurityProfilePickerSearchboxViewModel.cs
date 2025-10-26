using ServiceXpert.Web.Models.Security;

namespace ServiceXpert.Web.ViewModels;
public class SecurityProfilePickerSearchboxViewModel(string field, SecurityProfile? securityProfile = null)
{
    public string Label { get; } = field;

    public string HiddenInputName { get; } = string.Concat(field, "Id");

    public SecurityProfile? SecurityProfile { get; } = securityProfile;
}
