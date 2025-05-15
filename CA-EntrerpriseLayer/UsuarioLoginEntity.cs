using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_EntrerpriseLayer
{
    public class UsuarioLoginEntity
    {
        public int nIdUsuario { get; set; }
        public string sCorreoElectronico { get; set; } = string.Empty;
        public string sNombreCompleto { get; set; } = string.Empty;
        public string sNombres { get; set; } = string.Empty;
        public string sApellidoPaterno { get; set; } = string.Empty;
        public int nIdRol { get; set; }
        public bool bCambiarClave { get; set; }

        public List<PermisosPorRolEntity> Permissions { get; set; } = new();
    }

}
