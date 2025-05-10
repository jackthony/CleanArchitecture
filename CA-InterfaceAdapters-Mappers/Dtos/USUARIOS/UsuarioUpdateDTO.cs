using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.Dtos.USUARIOS
{
    public class UsuarioUpdateDTO
    {

        public int nIdUsuario { get; set; }
        public int nIdCargo { get; set; }

        public int nIdRol { get; set; }

        public int nEstado { get; set; }

        public DateTime? dtFechaModificacion { get; set; }

        public string? sUsuarioModificacion { get; set; }
    }
}
