using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models
{
    public class PagosDTO
    {
        public int Id { get; set; }
        public string ConceptoPago { get; set; }
        public double Monto { get; set; }
        public int DiasAtraso { get; set; }
        public bool PagoCubierto { get; set; }
        public string Periodo { get; set; }
        public bool PeriodoActual { get; set; }
        public DateTime FechaLimitedePago { get; set; }
    }
}
