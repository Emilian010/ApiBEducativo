using Api.Models;
using Api.Models.User;

namespace Api.Business.Contracts
{
    public interface IJwtGenerador
    {
        string GenerateToken(UsuarioDTO usuario);
        bool IsValid(string token);
    }
}
