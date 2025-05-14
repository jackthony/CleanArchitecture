using CA_ApplicationLayer;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Mappers.Dtos.Dir_Director;
using CA_InterfaceAdapters_Mappers.Dtos.EMP_EMPRESA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.Dir_Director
{
    public class Dir_DirectorCreateMapper : IMapper<Dir_DirectorCreateDTO, Dir_DirectorEntity>
    {
        public Dir_DirectorEntity ToEntity(Dir_DirectorCreateDTO dto)
            => new Dir_DirectorEntity()
            {
                nIdEmpresa = dto.nIdEmpresa,
                nTipoDocumento = dto.nTipoDocumento,
                sNumeroDocumento = dto.sNumeroDocumento,
                sNombres = dto.sNombres,
                sApellidos = dto.sApellidos,
                dFechaNacimiento = dto.dFechaNacimiento,
                nGenero = dto.nGenero,
                sDistrito = dto.sDistrito,
                sProvincia = dto.sProvincia,
                sDepartamento = dto.sDepartamento,
                sDireccion = dto.sDireccion,
                sTelefono = dto.sTelefono,
                sCorreo = dto.sCorreo,
                nCargo = dto.nCargo,
                nTipoDirector = dto.nTipoDirector,
                sProfesion = dto.sProfesion,
                mDieta = dto.mDieta,
                nEspecialidad = dto.nEspecialidad,
                dFechaNombramiento = dto.dFechaNombramiento,
                dFechaDesignacion = dto.dFechaDesignacion,
                //dFechaRenuncia = dto.dFechaRenuncia,
                sComentario = dto.sComentario,
                nUsuarioRegistro = dto.nUsuarioRegistro,
            };
    }
}
