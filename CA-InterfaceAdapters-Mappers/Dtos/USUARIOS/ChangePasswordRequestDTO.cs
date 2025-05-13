using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.Dtos.USUARIOS
{
    public class ChangePasswordRequestDTO
    {
        public int user { get; set; }
        public string password { get; set; }

        public string sUsuarioModificacion { get; set; }
    }
}
