using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Models
{
    public class UsuarioModel
    {
        [Key]
        public int nIdUsuario { get; set; }

        public string sNombresApellidos { get; set; } = string.Empty;

        public string sContrasena { get; set; } = string.Empty;

        public int nIdCargo { get; set; }

        public int nIdRol { get; set; }

        public string sCorreoElectronico { get; set; } = string.Empty;

        public bool bActivo { get; set; } = true;

        public DateTime dtFechaRegistro { get; set; }

        public string sUsuarioRegistro { get; set; } = string.Empty;

        public DateTime? dtFechaModificacion { get; set; }

        public string? sUsuarioModificacion { get; set; }
    }
}
