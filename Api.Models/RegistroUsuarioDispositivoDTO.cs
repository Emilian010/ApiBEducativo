using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models
{
    public class RegistroUsuarioDispositivoDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string DispositivoId { get; set; }
    }
}
