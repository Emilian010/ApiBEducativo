using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models
{
    public class CalificacionesDTO
    {
        public int Id { get; set; }
        public string Materia { get; set; }
        public string Calificacion { get; set; }
        public string Periodo { get; set; }
        
    }
}
