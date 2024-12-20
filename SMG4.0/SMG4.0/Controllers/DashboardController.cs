using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SMG4._0.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : APIBaseController
    {
        public DashboardController()
        {
            
        }

        [HttpGet("dashboard/{id}")]
        public async Task<IActionResult> GetEmployeeDashboard([FromRoute]long id, CancellationToken cancellationToken)
        {
            return Ok(id);
        }
    }
}
