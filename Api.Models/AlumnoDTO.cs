using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models
{
    public class AlumnoDTO
    {
        public int Id { get; set; }
        public int AlumnoInscritoId { get; set; }
        public string NombreCompleto { get; set; }
        public int GrupoId { get; set; }
        public string GrupoNombre { get; set; }
        public int GradoId { get; set; }
        public string GradoNombre { get; set; }
        public int NivelEducativoId { get; set; }
        public string NivelEducativoNombre { get; set; }
        public string Matricula { get; set; }
        public string UrlFoto { get; set; }
        public DateTime HoraSalida { get; set; }
        public string LugarSalida { get; set; }
        public string InstruccionesSalida { get; set; }

        List<PagosDTO> ListaPagos { get; set; }
        List<CalificacionesDTO> ListaCalificaciones { get; set; }
    }
}
