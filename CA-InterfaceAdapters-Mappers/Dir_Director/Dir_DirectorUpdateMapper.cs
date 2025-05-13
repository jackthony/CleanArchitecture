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
    public class Dir_DirectorUpdateMapper : IMapper<Dir_DirectorUpdateDTO, Dir_DirectorEntity>
    {
        public Dir_DirectorEntity ToEntity(Dir_DirectorUpdateDTO dto)
            => new Dir_DirectorEntity()
            {
                nIdRegistro = dto.nIdRegistro,
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
                dFechaRenuncia = dto.dFechaRenuncia,
                sComentario = dto.sComentario,
                sUsuarioModificacion = dto.sUsuarioModificacion
            };
    }
}
