using Api.Models.User;

namespace Api.Business.Contracts
{
    public interface IJwtGenerador
    {
        string GenerateToken(UsuarioData usuario);
        bool IsValid(string token);
    }
}
