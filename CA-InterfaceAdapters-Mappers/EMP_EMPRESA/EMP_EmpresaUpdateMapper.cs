using CA_ApplicationLayer;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Mappers.Dtos.EMP_EMPRESA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.EMP_EMPRESA
{
    public class EMP_EmpresaUpdateMapper : IMapper<EMP_EmpresaUpdateDTO, EMP_EmpresaEntity>
    {
        public EMP_EmpresaEntity ToEntity(EMP_EmpresaUpdateDTO dto)
            => new EMP_EmpresaEntity()
            {
                nIdEmpresa=dto.nIdEmpresa,
                sIdDepartamento=dto.sIdDepartamento,
                sIdProvincia=dto.sIdProvincia,
                sIdDistrito=dto.sIdDistrito,
                sDireccion=dto.sDireccion,
                sComentario=dto.sComentario,
                mIngresosUltimoAnio=dto.mIngresosUltimoAnio,
                mUtilidadUltimoAnio=dto.mUtilidadUltimoAnio,
                mConformacionCapitalSocial=dto.mConformacionCapitalSocial,
                bRegistradoMercadoValores=dto.bRegistradoMercadoValores,
                bActivo=dto.bActivo,
                nUsuarioModificacion=dto.nUsuarioModificacion
            };
    }
}
