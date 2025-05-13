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
    public class LoginResponseMapper : IMapper<UsuarioModel, LoginResponseDTO>
    {
        public LoginResponseDTO ToEntity(UsuarioModel model)
        => new LoginResponseDTO()
        {
            email = model.sCorreoElectronico,
            nombreCompleto = model.sNombreCompleto,
            primerNombre = LoginHelper.ObtenerPrimerNombre(model.sNombres),
            nombreVisual = LoginHelper.ObtenerNombreVisual(model.sNombres, model.sApellidoPaterno)
        };
    }
}
