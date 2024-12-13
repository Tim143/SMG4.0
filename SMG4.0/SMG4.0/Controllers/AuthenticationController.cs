using Microsoft.AspNetCore.Http;
using SMG4._0.Services;
using SMG4._0.Models.Request;
using Microsoft.AspNetCore.Mvc;
using SMG4._0.Models.Response;
using System.Threading;

namespace SMG4._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : APIBaseController
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<AuthenticationResponseModel> Login(AuthenticationRequestModel requestModel, CancellationToken cancellationToken)
        {
            return await authenticationService.LoginAsync(requestModel, cancellationToken);
        }

        [HttpPost("refresh")]
        public async Task<AuthenticationResponseModel> Refresh(string token, CancellationToken cancellationToken)
        {
            return await authenticationService.RefreshAsync(token, cancellationToken);
        }

        [HttpPost("logout")]
        public async Task Logout(CancellationToken cancellationToken)
        {
            await Task.Delay(100);
        }
    }
}
