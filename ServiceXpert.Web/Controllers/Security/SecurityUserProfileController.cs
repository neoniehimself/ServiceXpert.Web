using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Web.Enums.Security;

namespace ServiceXpert.Web.Controllers.Security;
[Route("SecurityUserProfiles")]
public class SecurityUserProfileController : SxpController
{
    [Authorize(Policy = nameof(SecurityPolicy.AdminOnly))]
    public IActionResult Index()
    {
        return View();
    }
}
