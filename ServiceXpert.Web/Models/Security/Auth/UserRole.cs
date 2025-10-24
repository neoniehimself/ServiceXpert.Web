using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Web.Models.Security.Auth;
public class UserRole
{
    /// <summary>
    /// User's UserName
    /// </summary>
    [Required]
    public required string UserName { get; set; }

    [Required]
    public required string RoleName { get; set; }
}
