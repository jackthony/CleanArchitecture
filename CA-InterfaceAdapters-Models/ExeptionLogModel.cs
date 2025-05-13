using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Models
{
    public class ExeptionLogModel
    {
        [Key]
        public int IdLog { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Mensaje { get; set; } = "";
        public string? Code { get; set; }
    }
}
