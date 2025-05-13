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
    public class LoginRequestMapper : IMapper<LoginRequestDTO, UsuarioEntity>
    {
            public UsuarioEntity ToEntity(LoginRequestDTO dto)
            => new UsuarioEntity()
            {
                sCorreoElectronico = dto.email,
                sContrasena = dto.password
            };
    }
}
