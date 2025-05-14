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
    public class UsuarioChangePasswordMapper : IMapper<UsuarioUpdatePasswordDTO, UsuarioEntity>
    {
        public UsuarioEntity ToEntity(UsuarioUpdatePasswordDTO dto)
            => new UsuarioEntity()
            {
                nIdUsuario = dto.nIdUsuario,
                nuevaClave = dto.nuevaClave,
                repetirClave = dto.repetirClave,
                nUsuarioModificacion = dto.nUsuarioModificacion
            };
    }
}
