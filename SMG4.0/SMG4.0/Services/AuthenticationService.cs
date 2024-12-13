using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
using SMG4._0.Helpers;
using SMG4._0.Models.Request;
using SMG4._0.Models.Response;
using System.IdentityModel.Tokens.Jwt;

namespace SMG4._0.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponseModel> LoginAsync(AuthenticationRequestModel model, CancellationToken cancellationToken);
        Task<AuthenticationResponseModel> RefreshAsync(string token, CancellationToken cancellationToken);
    }

    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService()
        {

        }

        public async Task<AuthenticationResponseModel> LoginAsync(AuthenticationRequestModel model, CancellationToken cancellationToken)
        {
            await Task.Delay(10);

            throw new NotImplementedException();
        }

        public async Task<AuthenticationResponseModel> RefreshAsync(string token, CancellationToken cancellationToken)
        {
            await Task.Delay(10);

            throw new NotImplementedException();
        }
    }
}
