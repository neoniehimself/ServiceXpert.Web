using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Web.Models.Security;
public class Login
{
    [Required]
    public required string UserName { get; set; }

    [Required]
    public required string Password { get; set; }
}
