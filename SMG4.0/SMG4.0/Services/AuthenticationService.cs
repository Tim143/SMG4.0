using AutoMapper;
using Azure.Core;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
using SMG4._0.Helpers;
using SMG4._0.Models.DTO;
using SMG4._0.Models.Request;
using SMG4._0.Models.Response;
using SMG4._0.Models.SubModels;
using SMG4._0.Repositories;
using System.IdentityModel.Tokens.Jwt;

namespace SMG4._0.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponseModel> LoginAsync(AuthenticationRequestModel model, CancellationToken cancellationToken);
        Task<AuthenticationResponseModel> RefreshAsync(string token, CancellationToken cancellationToken);
        Task<ActivateAccountResponseModel> ActivateAsync(ActivateEmployeeRequestModel model, CancellationToken cancellationToken);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IAuthenticationHelper authenticationHelper;
        private readonly ITokenRepository tokenRepository;

        public AuthenticationService(IEmployeeRepository employeeRepository, IAuthenticationHelper authenticationHelper, ITokenRepository tokenRepository)
        {
            this.employeeRepository = employeeRepository;
            this.authenticationHelper = authenticationHelper;
            this.tokenRepository = tokenRepository;
        }

        public async Task<ActivateAccountResponseModel> ActivateAsync(ActivateEmployeeRequestModel model, CancellationToken cancellationToken)
        {
            var employee = await employeeRepository.GetByEmailAsync(model.Email, cancellationToken);

            string activationCode = await employeeRepository.GetEmployeeActivationCode(employee.Id, cancellationToken);

            if (string.IsNullOrEmpty(activationCode))
            {
                return new ActivateAccountResponseModel();
            }

            PasswordHashModel hashModel = authenticationHelper.CreatePasswordHash(model.Password);

            employee.PasswordSalt = hashModel.PasswordSalt;
            employee.PasswordHash = hashModel.PasswordHash;

            await employeeRepository.UpdateEmployeeAsync(employee, cancellationToken);
            await employeeRepository.UpdateEmployeeActivationCode(employee.Id, cancellationToken);

            return new ActivateAccountResponseModel() { IsSucceeded = true};
        }

        public async Task<AuthenticationResponseModel> LoginAsync(AuthenticationRequestModel model, CancellationToken cancellationToken)
        {
            EmployeeDTO employee = await employeeRepository.GetByEmailAsync(model.Email, cancellationToken);

            if (employee == null)
            {
                return new AuthenticationResponseModel();
            }

            bool isPasswordCorrecrt = authenticationHelper.VerifyPasswordhash(model.Password, employee.PasswordHash, employee.PasswordSalt);

            if (!isPasswordCorrecrt)
            {
                return new AuthenticationResponseModel();
            }

            RefreshTokenModel expiredToken = await tokenRepository.GetRefreshTokenAsync(employee.Id, cancellationToken);

            if(expiredToken != null)
            {
                await tokenRepository.DeleteRefreshTokenAsync(expiredToken.Id, cancellationToken);
            }

            string accessToken = authenticationHelper.GenerateAccessToken(employee);
            string refreshToken = authenticationHelper.GenerateRefreshToken();

            await tokenRepository.CreateAsync(new RefreshTokenModel { EmployeeId = employee.Id, Token = refreshToken }, cancellationToken);

            return new AuthenticationResponseModel()
            {
                Id = employee.Id,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        public async Task<AuthenticationResponseModel> RefreshAsync(string token, CancellationToken cancellationToken)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            TokenValidationParameters parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = AppSecret.AuthSecret.ISSUER,
                ValidateAudience = true,
                ValidAudience = AppSecret.AuthSecret.AUDIENCE,
                ValidateLifetime = true,
                IssuerSigningKey = AppSecret.AuthSecret.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                handler.ValidateToken(token, parameters, out SecurityToken validatedToken);
            }
            catch (Exception ex)
            {
                return new AuthenticationResponseModel()
                {
                    Errors = $"{ex.Message}"                    
                };
            }

            var refreshToken = await tokenRepository.GetRefreshTokenAsync(token, cancellationToken);

            if (refreshToken == null)
            {
                return new AuthenticationResponseModel()
                {
                    Errors = "Invalid refresh token"
                };
            }

            await tokenRepository.DeleteRefreshTokenAsync(refreshToken.Id, cancellationToken);

            var currentUser = await employeeRepository.GetByIdAsync(refreshToken.EmployeeId, cancellationToken);

            if (currentUser == null)
            {
                return new AuthenticationResponseModel()
                {
                    Errors = "User not found"
                };
            }

            var currentAccessToken = authenticationHelper.GenerateAccessToken(currentUser);
            var currentRefreshToken = authenticationHelper.GenerateRefreshToken();

            await tokenRepository.CreateAsync(new RefreshTokenModel { Token = currentRefreshToken, EmployeeId = currentUser.Id }, cancellationToken);

            return new AuthenticationResponseModel()
            {
                Id = currentUser.Id,
                RefreshToken = currentRefreshToken,
                AccessToken = currentAccessToken
            };
        }
    }
}
