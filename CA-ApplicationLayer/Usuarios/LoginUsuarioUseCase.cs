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

        public LoginUsuarioUseCase(IUsuarioRepository UsuarioEntity, IPresenterResponse<TOutDto, TOutPut> presenterResponse, IMapper<TDTO, UsuarioEntity> mapper, IMapper<UsuarioModel, TOutDto> mapperResponse)
        {
            _usurioRepository = UsuarioEntity;
            _presenterResponse = presenterResponse;
            _mapper = mapper;
            _mapperResponse = mapperResponse;
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
