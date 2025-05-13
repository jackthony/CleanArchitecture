using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.Dtos.Dir_Director
{
    public class Dir_DirectorCreateDTO
    {
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
        public string sCorreo { get; set; }
        public int nCargo { get; set; }
        public int nTipoDirector { get; set; }
        public string sProfesion { get; set; }
        public decimal mDieta { get; set; }
        public int nEspecialidad { get; set; }
        public DateTime dFechaNombramiento { get; set; }
        public DateTime dFechaDesignacion { get; set; }
        public DateTime dFechaRenuncia { get; set; }
        public string sComentario { get; set; }
        public string sUsuarioRegistro { get; set; }
    }
}
