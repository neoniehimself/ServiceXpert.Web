namespace ServiceXpert.Web.Models;
public class AspNetUser : ModelBase<Guid>
{
    public string UserName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}
