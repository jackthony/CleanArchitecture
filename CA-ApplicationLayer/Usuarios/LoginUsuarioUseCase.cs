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
    public class LoginUsuarioUseCase<TDTO, TOutPut, TOutDto>
    {
        private readonly IUsuarioRepository _usurioRepository;
        private readonly IMapper<TDTO, UsuarioEntity> _mapper;
        private readonly IMapper<UsuarioModel, TOutDto> _mapperResponse;
        private readonly IPresenterResponse<TOutDto, TOutPut> _presenterResponse;
        private readonly IRolRepository _rolRepository;
        private readonly IRolPermisoRepository _rolPermisoRepository;
        private readonly IPermisoRepository _permisoRepository;
        private readonly IProcesoRepository _procesoRepository;

        public LoginUsuarioUseCase(IUsuarioRepository UsuarioEntity, IPresenterResponse<TOutDto, TOutPut> presenterResponse, IMapper<TDTO, UsuarioEntity> mapper, IMapper<UsuarioModel, TOutDto> mapperResponse, IRolRepository rolRepository, IRolPermisoRepository rolPermisoRepository, IProcesoRepository procesoRepository, IPermisoRepository permisoRepository)
        {
            _usurioRepository = UsuarioEntity;
            _presenterResponse = presenterResponse;
            _mapper = mapper;
            _mapperResponse = mapperResponse;
            _rolRepository = rolRepository; //IMPLEMENTAR EN EL SERVICIO
            _rolPermisoRepository = rolPermisoRepository; //IMPLEMENTAR SERVICIO
            _procesoRepository = procesoRepository;
            _permisoRepository = permisoRepository;
        }
        public async Task<IResult> ExecuteAsync(TDTO entity)
        {
            try
            {
                TOutDto? userValidate = default;
                var user = _mapper.ToEntity(entity);
                UsuarioModel userModel = await _usurioRepository.GetByEmailAsync(user);
                if (userModel == null) return Results.Ok(_presenterResponse.Present(userValidate));

                bool credencialesValidas = user.sContrasena == userModel.sContrasena;
                if (!credencialesValidas)
                    return Results.Ok(_presenterResponse.Present(userValidate));


                /*JALAR DATOS*/
                if(userModel.nIdRol != 0 && userModel.nIdRol != null)
                {
                    var rol = await this._rolRepository.GetById(userModel.nIdRol);

                    var rolPermisos = await this._rolPermisoRepository.GetPermisosPorRolAsync(userModel.nIdRol);

                    List<PermisoModel> permisos = new List<PermisoModel>();
                    List<int> idPermisos = new List<int>();
                    //List<int> idPermisos = new List<int>();

                    foreach (var rolPermiso in rolPermisos)
                    {
                        var permiso = await this._permisoRepository.GetById(rolPermiso.nIdPermiso);
                        permisos.Add(permiso);
                        if (!(idPermisos.Contains(permiso.nIdProceso)))
                        {
                            idPermisos.Add(permiso.nIdProceso);
                        }
                    }

                    List<ProcesoModel> procesos = new List<ProcesoModel>();

                    foreach (var idPermiso in idPermisos)
                    {
                        var proceso = await this._procesoRepository.GetById(idPermiso);
                        procesos.Add(proceso);
                        //var permiso = await this._permisoRepository.GetById(rolPermiso.nIdPermiso);
                        //permisos.Add(permiso);
                    }





                } else
                {
                    //error
                }
                    //userModel.permission = object;

                    userValidate = _mapperResponse.ToEntity(userModel);

                var response = _presenterResponse.Present(userValidate);

                //UsuarioModel a Usuario


                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    IsSuccess = false,
                    Errors = new List<string> { ex.Message }
                };
                return Results.BadRequest(errorResponse);
            }
        }
    }
}
