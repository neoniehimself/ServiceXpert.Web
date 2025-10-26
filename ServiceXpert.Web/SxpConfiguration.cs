using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Web;
public class SxpConfiguration
{
    [Required(ErrorMessage = "Fatal: Missing Jwt key")]
    public string JwtSecretKey { get; set; } = string.Empty;
}
