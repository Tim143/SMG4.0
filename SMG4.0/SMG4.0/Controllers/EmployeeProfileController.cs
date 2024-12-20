using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMG4._0.Models.Response;
using SMG4._0.Services;

namespace SMG4._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeProfileController : APIBaseController
    {
        private readonly IEmployeeProfileService employeeProfileService;

        public EmployeeProfileController(IEmployeeProfileService employeeProfileService)
        {
            this.employeeProfileService = employeeProfileService;
        }

        [HttpGet("profile/{id}")]
        public async Task<EmployeeProfileResponseModel> GetEmployeeProfile(long id, CancellationToken cancellationToken)
        {
            return await employeeProfileService.GetProfile(id, cancellationToken);
        }

        [HttpPost("profile")]
        public async Task<EmployeeProfileResponseModel> UpdateEmployeeProfile(long id, CancellationToken cancellationToken)
        {
            return new();
        }
    }
}
