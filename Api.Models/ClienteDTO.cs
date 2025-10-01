using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Rfc { get; set; }
        public int ImplementacionId { get; set; }
        public bool Status { get; set; }

        public string UrlLogo { get; set; }
    }
}
