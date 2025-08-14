using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace DevMatch.Helpers
{
    public static class TokenHelpers
    {
        public static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration)
        {
            var tokenKey = Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]);
            return new TokenValidationParameters
            {
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidateAudience = true,
                ValidAudience = configuration["JwtSettings:Audience"],
                ValidateIssuer = true,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(tokenKey),
                RoleClaimType = ClaimTypes.Role
            };
        }
    }
}
