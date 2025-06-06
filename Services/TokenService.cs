﻿using DevMatch.Enums;
using DevMatch.Helpers;
using DevMatch.Interfaces;
using DevMatch.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace DevMatch.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        public TokenService(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public string GenerateRereshToken(User usuario)
        {
            var JwtSettings = _configuration.GetSection("JwtSettings");
            var SecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings["SecretKey"]));

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuario.Name),
                new Claim(ClaimTypes.Email, usuario.Email),
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

        public async Task<string> GenerateToken(User usuario)
        {
            var JwtSettings = _configuration.GetSection("JwtSettings");
            var SecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings["SecretKey"]));

            var claims = new List<Claim>()
            {
                 new Claim(ClaimTypes.Name, usuario.Name),
                new Claim(ClaimTypes.Email, usuario.Email),  
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };

            var roles = await _userManager.GetRolesAsync(usuario);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
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
