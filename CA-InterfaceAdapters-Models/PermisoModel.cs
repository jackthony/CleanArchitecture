using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Models
{
    [Table("PERMISO")]
    public class PermisoModel
    {
        [Key]
        public int nIdPermiso { get; set; }

        [Required]
        [StringLength(100)]
        public string sNombrePermiso { get; set; } = null!;

        [StringLength(250)]
        public string? sDescripcion { get; set; }
        public int nIdProceso { get; set; }

        public bool bActivo { get; set; } = true;

        public DateTime dtFechaCreacion { get; set; }

        public int nIdUsuarioCreacion { get; set; }

        public DateTime? dtFechaModificacion { get; set; }

        public int? nIdUsuarioModificacion { get; set; }

        public bool bEliminado { get; set; } = false;

        //// Navegación
        //public ProcesoModel? Proceso { get; set; }
        //public ICollection<RolPermisoModel>? RolPermisos { get; set; }
    }
}
