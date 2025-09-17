namespace ServiceXpert.Web.Models.AspNetUserProfile;
public class AspNetUserProfile : ModelBase<Guid>
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string FirstNameLastName { get => $"{this.FirstName} {this.LastName}"; }
}
