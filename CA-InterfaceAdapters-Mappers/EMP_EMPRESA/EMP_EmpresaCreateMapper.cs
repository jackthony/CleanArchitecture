using CA_ApplicationLayer;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Mappers.Dtos.EMP_EMPRESA;
using CA_InterfaceAdapters_Mappers.Dtos.Requests;

namespace CA_InterfaceAdapters_Mappers.EMP_EMPRESA
{
    public class EMP_EmpresaCreateMapper : IMapper<EMP_EmpresaCreateDTO, EMP_EmpresaEntity>
    {
        public EMP_EmpresaEntity ToEntity(EMP_EmpresaCreateDTO dto)
            => new EMP_EmpresaEntity()
            {
                sNombreEmpresa = dto.sNombreEmpresa,
                sRuc = dto.sRuc,
                sRazonSocial = dto.sRazonSocial,
                nIdProponente = dto.nIdProponente,
                nIdRubroNegocio = dto.nIdRubroNegocio,
                sIdDepartamento = dto.sIdDepartamento,
                sIdProvincia = dto.sIdProvincia,
                sIdDistrito = dto.sIdDistrito,
                sDireccion = dto.sDireccion,
                sComentario = dto.sComentario,
                mIngresosUltimoAnio = dto.mIngresosUltimoAnio,
                mUtilidadUltimoAnio = dto.mUtilidadUltimoAnio,
                mConformacionCapitalSocial = dto.mConformacionCapitalSocial,
                nNumeroMiembros = dto.nNumeroMiembros,
                bRegistradoMercadoValores = dto.bRegistradoMercadoValores,
                bActivo = dto.bActivo,
                sUsuarioRegistro = dto.sUsuarioRegistro
            };
    }
}
