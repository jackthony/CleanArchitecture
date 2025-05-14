using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Models
{
    public class UsuarioModel
    {
        [Key]
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

        public int? nUsuarioModificacion { get; set; }

        [NotMapped]
        public string? sCargoDescripcion { get; set; }

        [NotMapped]
        public string? sPerfilDescripcion { get; set; }

        [NotMapped]
        public string? sEstadoDescripcion { get; set; }
        [NotMapped]
        public string sNombreCompleto => $"{sApellidoPaterno} {sApellidoMaterno} {sNombres}";

        public bool bCambiarClave { get; set; }
    }
}
