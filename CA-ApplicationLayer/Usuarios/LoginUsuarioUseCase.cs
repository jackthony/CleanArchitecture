using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Usuarios
{
    public class LoginUsuarioUseCase<TDTO>
    {
        // Mantén solo lo esencial
        private readonly IUsuarioRepository _usurioRepository;
        private readonly IMapper<TDTO, UsuarioEntity> _mapper;
        private readonly IRolPermisoRepository _rolPermisoRepository;

        public LoginUsuarioUseCase(
            IUsuarioRepository usuarioRepository,
            IMapper<TDTO, UsuarioEntity> mapper,
            IRolPermisoRepository rolPermisoRepository)
        {
            _usurioRepository = usuarioRepository;
            _mapper = mapper;
            _rolPermisoRepository = rolPermisoRepository;
        }

        public async Task<UsuarioLoginEntity?> ExecuteAsync(TDTO entity)
        {
            var user = _mapper.ToEntity(entity);
            UsuarioModel userModel = await _usurioRepository.GetByEmailAsync(user);
            if (userModel == null) return null;

            bool credencialesValidas = user.sContrasena == userModel.sContrasena;
            if (!credencialesValidas) return null;

            var permisos = await _rolPermisoRepository.GetPermisosPorRolAsync(userModel.nIdRol);

            return new UsuarioLoginEntity
            {
                nIdUsuario = userModel.nIdUsuario,
                sCorreoElectronico = userModel.sCorreoElectronico,
                sNombreCompleto = $"{userModel.sApellidoPaterno} {userModel.sApellidoMaterno} {userModel.sNombres}",
                sNombres = userModel.sNombres,
                sApellidoPaterno = userModel.sApellidoPaterno,
                nIdRol = userModel.nIdRol,
                bCambiarClave = userModel.bCambiarClave,
                Permissions = permisos.ToList()
            };
        }
    }

}