using Microsoft.IdentityModel.Tokens;
using SMG4._0.Helpers;
using SMG4._0.Models.DTO;
using SMG4._0.Models.SubModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace SMG4._0.Services
{
    public interface IAuthenticationHelper
    {
        PasswordHashModel CreatePasswordHash(string password);
        bool VerifyPasswordhash(string password, byte[] passwordHash, byte[] passwordSalt);
        string GenerateRefreshToken();
        string GenerateAccessToken(EmployeeDTO employee);
    }

    public class AuthenticationHelper : IAuthenticationHelper
    {
        public string GenerateAccessToken(EmployeeDTO employee)
        {
            var securityKey = AppSecret.AuthSecret.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                new Claim(ClaimTypes.Name, employee.FirstName),
                new Claim(ClaimTypes.Surname, employee.LastName),
                new Claim(ClaimTypes.Email, employee.Email),
            };

            var token = new JwtSecurityToken(
                AppSecret.AuthSecret.ISSUER,
                AppSecret.AuthSecret.AUDIENCE,
                claims,
                expires: DateTime.Now.AddMinutes(2880),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public PasswordHashModel CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                return new PasswordHashModel()
                {
                    PasswordSalt = hmac.Key,
                    PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))
                };
            }
        }

        public bool VerifyPasswordhash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes((string)password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public string GenerateRefreshToken()
        {
            var securityKey = AppSecret.AuthSecret.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                AppSecret.AuthSecret.ISSUER,
                AppSecret.AuthSecret.AUDIENCE,
                expires: DateTime.Now.AddMinutes(43200),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
