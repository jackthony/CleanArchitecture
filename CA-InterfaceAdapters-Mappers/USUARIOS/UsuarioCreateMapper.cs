using CA_ApplicationLayer;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Mappers.Dtos.EMP_EMPRESA;
using CA_InterfaceAdapters_Mappers.Dtos.USUARIOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.USUARIOS
{
    public class UsuarioCreateMapper : IMapper<UsuarioCreateDTO, UsuarioEntity>
    {
        public UsuarioEntity ToEntity(UsuarioCreateDTO dto)
            => new UsuarioEntity()
            {
                sNombresApellidos = dto.sNombresApellidos,
                sContrasena = dto.sContrasena,
                nIdCargo = dto.nIdCargo,
                nIdRol = dto.nIdRol,
                sCorreoElectronico = dto.sCorreoElectronico,
                bActivo = dto.bActivo,
                sUsuarioRegistro = dto.sUsuarioRegistro
            };
    }
}
