using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.Dtos.Exeptions
{
    public class ExeptionLogCreateDTO
    {
        public string Mensaje { get; set; } = "";
        public string? Code { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
