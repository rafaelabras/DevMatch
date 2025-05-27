using DevMatch.Enums;
using DevMatch.Helpers;
using DevMatch.Interfaces;
using DevMatch.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace DevMatch.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateRereshToken(User usuario)
        {
            var JwtSettings = _configuration.GetSection("JwtSettings");
            var SecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings["SecretKey"]));

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Name),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };

            var tempoExpiracao = JwtSettings.GetValue<int>("RefereshExpirationTimeInMinutes");

            var cred = new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: JwtSettings.GetValue<string>("Issuer"), // quem emite
                audience: JwtSettings.GetValue<string>("Audience"), // quem consume
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(tempoExpiracao),
                signingCredentials: cred
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateToken(User usuario)
        {
            var JwtSettings = _configuration.GetSection("JwtSettings");
            var SecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings["SecretKey"]));

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Name),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };

            if (usuario.Role == UserRolesEnum.Mentorado)
            {
                claims.Add(new Claim(ClaimTypes.Role, usuario.Role.ToString()));
            }


            if (usuario.Role == UserRolesEnum.Mentor)
            {
                claims.Add(new Claim(ClaimTypes.Role, usuario.Role.ToString()));
            }


            if (usuario.Role == UserRolesEnum.Admin)
            {
                claims.Add(new Claim(ClaimTypes.Role, usuario.Role.ToString()));
            }

            var tempoExpiracao = JwtSettings.GetValue<int>("ExpirationTimeInMinutes");

            var cred = new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: JwtSettings.GetValue<string>("Issuer"), // quem emite
                audience: JwtSettings.GetValue<string>("Audience"), // quem consume
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(tempoExpiracao),
                signingCredentials: cred
                );
           
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<(bool IsValid, string nome, string email)> ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return (false, null, null);

            var tokenParameters = TokenHelpers.GetTokenValidationParameters(_configuration);

            var handler = new JwtSecurityTokenHandler();
            var validTokenResult = await new JwtSecurityTokenHandler().ValidateTokenAsync(token, tokenParameters);

            if (!validTokenResult.IsValid)
                return (false, null, null);


            var username = validTokenResult.Claims.FirstOrDefault(c => c.Key == JwtRegisteredClaimNames.Sub).Value as string;
            var email = validTokenResult.Claims.FirstOrDefault(c => c.Key == JwtRegisteredClaimNames.Email).Value as string;

            return (true, username, email);
        }
    }
}
