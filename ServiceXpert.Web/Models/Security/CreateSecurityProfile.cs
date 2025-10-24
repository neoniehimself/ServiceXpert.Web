using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Web.Models.Security;
public class CreateSecurityProfile : CreateModelBase
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    public CreateSecurityProfile(Guid id)
    {
        this.Id = id;
    }
}
