using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Data.Models
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre{ get; set; }
        public string Rfc { get; set; }
        public int ImplementacionId { get; set; }
        public bool Status { get; set; }
        public string UrlLogo { get; set; }
    }
}
