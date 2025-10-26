namespace ServiceXpert.Web.Models.Security;
public class SecurityProfile : ModelBase<Guid>
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string FirstNameLastName { get => $"{this.FirstName} {this.LastName}"; }
}
