namespace ServiceXpert.Web.Models.Security;
public class SecurityUser : ModelBase<Guid>
{
    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public string? PhoneNumber { get; set; }

    public bool IsActive { get; set; }

    public SecurityProfile? SecurityProfile { get; set; }
}
