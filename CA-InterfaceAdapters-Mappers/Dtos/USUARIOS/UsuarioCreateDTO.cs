using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.Dtos.USUARIOS
{
    public class UsuarioCreateDTO
    {
        public string sNombresApellidos { get; set; } = string.Empty;

        public string sContrasena { get; set; } = string.Empty;

        public int nIdCargo { get; set; }

        public int nIdRol { get; set; }

        public string sCorreoElectronico { get; set; } = string.Empty;

        public bool bActivo { get; set; } = true;

        public string sUsuarioRegistro { get; set; } = string.Empty;

    }
}
