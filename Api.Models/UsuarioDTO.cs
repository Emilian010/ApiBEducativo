using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models
{
    public class UsuarioDTO: IdentityUser
    {
        public string Token { get; set; } = string.Empty;
        public int UsuarioId { get; set; }
        public string NombreCompleto { get; set; }
        public int PerfilId { get; set; }
        public string PerfilNombre { get; set; }
        public bool Activo { get; set; }
        public int EscuelaId { get; set; }
        public string EscuelaNombre { get; set; }
        public double EscuelaLatitud { get; set; }
        public double EscuelaLongitud { get; set; }
        public int ImplementacionId { get; set; }
        public string TokenDispositivo { get; set; }
        public List<AlumnoDTO>  ListaAlumnos { get; set; }
        public List<RoleViewDTO> ListaRolView { get; set; }

    }
}
