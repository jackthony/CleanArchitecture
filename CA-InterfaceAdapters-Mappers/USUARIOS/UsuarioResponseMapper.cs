using CA_ApplicationLayer;
using CA_EntrerpriseLayer;
using CA_EntrerpriseLayer.Helpers;
using CA_InterfaceAdapters_Mappers.Dtos.USUARIOS;
using CA_InterfaceAdapters_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.USUARIOS
{
    public class UsuarioResponseMapper : IMapper<ItemsPaginatorEntity<UsuarioModel>, ItemsPaginatorEntity<UsuarioResponseDTO>>
    {
        public ItemsPaginatorEntity<UsuarioResponseDTO> ToEntity(ItemsPaginatorEntity<UsuarioModel> model)
        => new ItemsPaginatorEntity<UsuarioResponseDTO>()
        {
            lstItem = model.lstItem?.Select(userModelToDto) ?? Enumerable.Empty<UsuarioResponseDTO>(),
            PageIndex = model.PageIndex,
            PageSize = model.PageSize,
            TotalRows = model.TotalRows,
        };

        private UsuarioResponseDTO userModelToDto(UsuarioModel model) => new UsuarioResponseDTO()
        {
            nIdUsuario = model.nIdUsuario,
            sApellidoPaterno = model.sApellidoPaterno,
            sApellidoMaterno = model.sApellidoMaterno,
            sNombres = model.sNombres,
            nIdCargo = model.nIdCargo,
            nIdRol = model.nIdRol,
            sCorreoElectronico = model.sCorreoElectronico,
            nEstado = model.nEstado,
            dtFechaRegistro = model.dtFechaRegistro,
            nUsuarioRegistro = model.nUsuarioRegistro,
            dtFechaModificacion = model.dtFechaModificacion,
            nUsuarioModificacion = model.nUsuarioModificacion,
            sCargoDescripcion = model.sCargoDescripcion,
            sPerfilDescripcion = model.sPerfilDescripcion,
            sEstadoDescripcion = model.sEstadoDescripcion,
        };
    }
}
