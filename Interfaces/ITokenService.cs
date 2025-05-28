using DevMatch.Models;

namespace DevMatch.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User usuario);
        string GenerateRereshToken(User usuario);
        Task <(bool IsValid, string nome, string email)> ValidateToken (string token);
    }
}
