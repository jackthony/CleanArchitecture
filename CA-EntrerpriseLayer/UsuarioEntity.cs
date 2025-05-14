using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_EntrerpriseLayer
{
    public  class UsuarioEntity
    {
        public int nIdUsuario { get; set; }

        public string sApellidoPaterno { get; set; } = string.Empty;
        public string sApellidoMaterno { get; set; } = string.Empty;
        public string sNombres { get; set; } = string.Empty;

        public string sContrasena { get; set; } = string.Empty;

        public int nIdCargo { get; set; }

        public int nIdRol { get; set; }

        public string sCorreoElectronico { get; set; } = string.Empty;

        public int nEstado { get; set; }

        public DateTime dtFechaRegistro { get; set; }

        public int nUsuarioRegistro { get; set; }

        public DateTime? dtFechaModificacion { get; set; }
        public bool bCambiarClave { get; set; }

        public int? nUsuarioModificacion { get; set; }

        public string antiguaClave { get; set; } = string.Empty;
        public string nuevaClave { get; set; } = string.Empty;
        public string repetirClave { get; set; } = string.Empty;
        public bool? cambioClave { get; set; }
    }
}
