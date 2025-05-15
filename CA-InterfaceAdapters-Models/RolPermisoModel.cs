using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Models
{
    [Table("ROL_PERMISO")]
    public class RolPermisoModel
    {
        [Key, Column(Order = 0)]
        [ForeignKey("RolModel")]
        public int nIdRol { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("PermisoModel")]
        public int nIdPermiso { get; set; }

        public DateTime dtFechaAsignacion { get; set; }

        public int nIdUsuarioAsignacion { get; set; }
    }
}
