using DevMatch.Enums;
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
    }
}
