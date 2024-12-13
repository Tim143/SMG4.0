using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace SMG4._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIBaseController : ControllerBase
    {
        protected int GetCurrentuserId()
        {
            var claims = HttpContext.User.Claims as ClaimsPrincipal;

            if(claims == null)
            {
                throw new UnauthorizedAccessException();
            }

            var claimEmployeeId = claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if(claimEmployeeId == null)
            {
                throw new UnauthorizedAccessException();
            }

            if (!int.TryParse(claimEmployeeId.Value, out int employeeId))
            {
                throw new UnauthorizedAccessException();
            }

            return employeeId;
        }
    }
}
