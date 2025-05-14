using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Models
{
    public class Dir_DirectorModel
    {
        [Key]
        public int nIdRegistro { get; set; }
        public int nIdEmpresa { get; set; }
        public int nTipoDocumento { get; set; }
        public string sNumeroDocumento { get; set; }
        public string sNombres { get; set; }
        public string sApellidos { get; set; }
        public DateTime dFechaNacimiento { get; set; }
        public int nGenero { get; set; }
        public string sDistrito { get; set; }
        public string sProvincia { get; set; }
        public string sDepartamento { get; set; }
        public string sDireccion { get; set; }
        public string sTelefono { get; set; }
        public string? sTelefonoSecundario { get; set; }
        public string? sTelefonoTerciario { get; set; }
        public string sCorreo { get; set; }
        public string? sCorreoSecundario { get; set; }
        public string? sCorreoTerciario { get; set; }
        public int nCargo { get; set; }
        public int nTipoDirector { get; set; }
        public string sProfesion { get; set; }
        public decimal mDieta { get; set; }
        public int nEspecialidad { get; set; }
        public DateTime dFechaNombramiento { get; set; }
        public DateTime dFechaDesignacion { get; set; }
        public DateTime? dFechaRenuncia { get; set; }
        public string? sComentario { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public int nUsuarioRegistro { get; set; }
        public DateTime? dtFechaModificacion { get; set; }
        public int? nUsuarioModificacion { get; set; }

        public string sNombreCompleto => $"{sNombres} {sApellidos}";

        [NotMapped]
        public string? sTipoDocumentoDescripcion { get; set; }
        [NotMapped]
        public string? sCargoDescripcion { get; set; }
        [NotMapped]
        public string? sTipoDirectorDescripcion { get; set; }
    }
}
