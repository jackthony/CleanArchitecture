using CA_ApplicationLayer;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Mappers.Dtos.Exeptions;
using CA_InterfaceAdapters_Mappers.Dtos.USUARIOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.EXEPTIONS
{
    public class ExeptionCreateMapper : IMapper<ExeptionLogCreateDTO, ExeptionLogEntity>
    {
        public ExeptionLogEntity ToEntity(ExeptionLogCreateDTO dto)
        {
            return new ExeptionLogEntity()
            {
                Code = dto.Code,
                FechaRegistro = dto.FechaRegistro,
                Mensaje = dto.Mensaje,
            };
        }
    }
}
