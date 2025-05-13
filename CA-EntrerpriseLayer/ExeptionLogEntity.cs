using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_EntrerpriseLayer
{
    public class ExeptionLogEntity
    {
        public string Mensaje { get; set; } = "";
        public string? Code { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
