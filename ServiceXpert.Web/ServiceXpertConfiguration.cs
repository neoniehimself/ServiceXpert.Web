using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Web;
public class ServiceXpertConfiguration
{
    [Required(ErrorMessage = "Fatal: Missing Jwt key")]
    public string JwtSecretKey { get; set; } = string.Empty;
}
