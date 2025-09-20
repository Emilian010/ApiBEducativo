using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace Api.Data.Models
{
    public class RoleView
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleViewId { get; set; }

        [ForeignKey("AspNetRoles")]
        [MaxLength(450)]
        public string RolesId { get; set; }

        [ForeignKey("View")]
        public int ViewId { get; set; }

        public IdentityRole Roles { get; set; }

        public View Views { get; set; }

        [DisplayName("Ver")]
        public bool IsView { get; set; }

        [DisplayName("Crear")]
        public bool IsAdd { get; set; }

        [DisplayName("Actualizar")]
        public bool IsUpdate { get; set; }

        [DisplayName("Eliminar")]
        public bool IsDelete { get; set; }

    }
}
