using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Models
{
    public class ArchivoProcesoModel
    {
        [Key]
        public int nIdArchivoProceso { get; set; }

        public string sRutaFisica { get; set; } = null!;

        public string sExtension { get; set; } = null!;

        public int nIdEntidad { get; set; }

        public int nIdEntidadRelacionada { get; set; }

        public string? sDescripcion { get; set; }

        public DateTime dtFechaCreacion { get; set; }

        public int nIdUsuarioCreacion { get; set; }

        public DateTime? dtFechaModif { get; set; }

        public int? nIdUsuarioModif { get; set; }

        public bool bEliminado { get; set; } = false;

        public string sNombreFile { get; set; }

    }
}
