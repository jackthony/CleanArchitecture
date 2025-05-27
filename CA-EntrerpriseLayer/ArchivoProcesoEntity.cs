using Microsoft.AspNetCore.Http;                // Para IFormFile
using System.ComponentModel.DataAnnotations;    // Para [Required], validaciones
using System;                                   // Para DateTime


namespace CA_EntrerpriseLayer
{
    public class ArchivoProcesoEntity
    {
        public int nIdArchivoProceso { get; set; }

        public string sRutaFisica { get; set; } = null!;

        public string sExtension { get; set; } = null!;

        public int nIdEntidadRelacionada { get; set; }
        public string sIdEntidad { get; set; }

        public string? sDescripcion { get; set; }

        public DateTime dtFechaCreacion { get; set; }

        public int nIdUsuarioCreacion { get; set; }

        public DateTime? dtFechaModif { get; set; }

        public int? nIdUsuarioModif { get; set; }

        public bool bEliminado { get; set; } = false;

        public string sNombreFile { get; set; }

        public IFormFile? formFile { get; set; }
    }
}
