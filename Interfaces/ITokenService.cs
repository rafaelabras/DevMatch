using DevMatch.Models;

namespace DevMatch.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User usuario);
    }
}
