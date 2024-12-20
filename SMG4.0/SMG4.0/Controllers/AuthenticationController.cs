using Microsoft.AspNetCore.Http;
using SMG4._0.Services;
using SMG4._0.Models.Request;
using Microsoft.AspNetCore.Mvc;
using SMG4._0.Models.Response;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace SMG4._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : APIBaseController
    {
        private readonly IAuthenticationService authenticationService;
        private readonly ILogger<AuthenticationController> logger;

        public AuthenticationController(IAuthenticationService authenticationService, ILogger<AuthenticationController> logger)
        {
            this.authenticationService = authenticationService;
            this.logger = logger;
        }

        [HttpPost("login")]
        public async Task<AuthenticationResponseModel> Login(AuthenticationRequestModel requestModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return new();
            }

            return await authenticationService.LoginAsync(requestModel, cancellationToken);
        }

        [HttpPost("refresh")]
        public async Task<AuthenticationResponseModel> Refresh(string token, CancellationToken cancellationToken)
        {
            return await authenticationService.RefreshAsync(token, cancellationToken);
        }

        [HttpPost("activate")]
        public async Task<ActivateAccountResponseModel> ActivateAccount(ActivateEmployeeRequestModel requestModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return new();
            }

            return await authenticationService.ActivateAsync(requestModel, cancellationToken);
        }
    }
}
