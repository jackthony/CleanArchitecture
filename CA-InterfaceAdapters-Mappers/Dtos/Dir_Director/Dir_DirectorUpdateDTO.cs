using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.Dtos.Dir_Director
{
    public class Dir_DirectorUpdateDTO
    {
        public int nIdRegistro { get; set; }
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
        //public DateTime dFechaRenuncia { get; set; }
        public string sComentario { get; set; }
        public int nUsuarioModificacion { get; set; }
    }
}
