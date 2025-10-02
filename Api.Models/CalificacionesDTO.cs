using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models
{
    public class CalificacionesDTO
    {
        public int AlumnoMateria { get; set; }
        public string Materia { get; set; }
        public double Parcial1 { get; set; }
        public double Parcial2 { get; set; }
        public double Parcial3 { get; set; }
        public int Orden { get; set; }
        public string Observaciones { get; set; }
        public string Sugerencias { get; set; }
    }
}
