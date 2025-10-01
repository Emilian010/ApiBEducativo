using Api.Models;

namespace Api.Data.AspNet
{
    public partial class AspNetUser
    {
        public AspNetUser()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaim>();
            AspNetUserLogins = new HashSet<AspNetUserLogin>();
            AspNetUserTokens = new HashSet<AspNetUserToken>();
            Roles = new HashSet<AspNetRoles>();
        }

        public string Id { get; set; } = null!;
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; } = DateTimeOffset.UtcNow;
        public bool LockoutEnabled { get; set; }
        public int? AccessFailedCount { get; set; }

        public int UsuarioId { get; set; }
        public string NombreCompleto { get; set; }
        //public string Correo { get; set; }
        public int PerfilId { get; set; }
        public string PerfilNombre { get; set; }
        public bool Activo { get; set; }
        public int EscuelaId { get; set; }
        public string EscuelaNombre { get; set; }
        public double EscuelaLatitud { get; set; }
        public double EscuelaLongitud { get; set; }
        public int ImplementacionId { get; set; }
        public string TokenDispositivo { get; set; }

        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; }

        public virtual ICollection<AspNetRoles> Roles { get; set; }
    }
}
