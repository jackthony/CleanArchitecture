using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.Dtos.USUARIOS
{
    public class LoginResponseDTO
    {
        public string email { get; set; } = string.Empty;
        public string nombreCompleto { get; set; } = string.Empty;
        public string primerNombre { get; set; } = string.Empty;
        public string nombreVisual { get; set; } = string.Empty;
    }
}
