using CA_EntrerpriseLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Usuarios
{
    public class ChangePasswordUseCase<TDTO, TOutPut>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper<TDTO, UsuarioEntity> _mapper;
        private readonly ITimeZoneInfoProvider _tzProvider;
        private readonly IPresenterResponse<bool, TOutPut> _presenterResponse;

        public ChangePasswordUseCase(IUsuarioRepository usuarioRepository, IPresenterResponse<bool, TOutPut> presenterResponse, IMapper<TDTO, UsuarioEntity> mapper, ITimeZoneInfoProvider tzProvider)
        {
            _usuarioRepository = usuarioRepository;
            _presenterResponse = presenterResponse;
            _tzProvider = tzProvider;
            _mapper = mapper;
        }

        public async Task<IResult> ExecuteAsync(TDTO entity)
        {
            try
            {
                var user = _mapper.ToEntity(entity);
                bool validate = await _usuarioRepository.ValidarPassword(user);
                if (!validate)
                {
                    var errorResponse = new
                    {
                        IsSuccess = false,
                        Errors = new List<string> { "Contraseña incorrecta" }
                    };
                    return Results.BadRequest(errorResponse);
                }
                user.dtFechaModificacion = _tzProvider.GetCurrentTimeInZone();
                bool update = await _usuarioRepository.ChangePassword(user);
                var response = _presenterResponse.Present(update);

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
