using CA_ApplicationLayer;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Mappers.Dtos.USUARIOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.USUARIOS
{
    public class ChangePasswordMapper : IMapper<ChangePasswordRequestDTO, UsuarioEntity>
    {
        public UsuarioEntity ToEntity(ChangePasswordRequestDTO dto)
            => new UsuarioEntity()
            {
                nIdUsuario = dto.user,
                sContrasena = dto.password,
                nUsuarioModificacion = dto.nUsuarioModificacion
            };
    }
}
