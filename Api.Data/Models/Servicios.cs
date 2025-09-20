using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api.Data.Models
{
    public class Servicios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiciosId { get; set; }
        [MaxLength(150)]
        [Required(ErrorMessage = "Campo Requerido")]

        public string Nombre { get; set; } = string.Empty;
    }
}
