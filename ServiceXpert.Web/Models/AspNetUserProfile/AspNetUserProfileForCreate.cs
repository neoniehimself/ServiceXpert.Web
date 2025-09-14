using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Web.Models.AspNetUserProfile;
public class AspNetUserProfileForCreate : ModelBaseForCreate
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    public AspNetUserProfileForCreate(Guid id)
    {
        this.Id = id;
    }
}
