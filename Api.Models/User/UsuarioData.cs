using Microsoft.AspNetCore.Identity;

namespace Api.Models.User
{
    public class UsuarioData : IdentityUser
    {
        public string Token { get; set; } = string.Empty;

        public Rol? Rol { get; set; }
        public string RolId { get; set; }
    }
}
