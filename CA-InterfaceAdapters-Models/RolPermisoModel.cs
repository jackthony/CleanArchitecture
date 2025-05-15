using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Models
{
    public class RolPermisoModel
    {
        public int nIdRol { get; set; }

        public int nIdPermiso { get; set; }

        public DateTime dtFechaAsignacion { get; set; }

        public int nIdUsuarioAsignacion { get; set; }
    }
}
