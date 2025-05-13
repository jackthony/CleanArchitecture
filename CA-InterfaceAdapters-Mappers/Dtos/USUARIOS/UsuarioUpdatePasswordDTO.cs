using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.Dtos.USUARIOS
{
    public class UsuarioUpdatePasswordDTO
    {
        public int nIdUsuario { get; set; }
        public string antiguaClave { get; set; }
        public string nuevaClave { get; set; }
        public string repetirClave { get; set; }
        public string? sUsuarioModificacion { get; set; }
        public DateTime? dtFechaModificacion { get; set; }
        public bool? bCambioClave { get; set; }
    }
}
