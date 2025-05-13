using CA_ApplicationLayer.EMP_Empresa;
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
    public class AddUsuarioUseCase<TDTO, TOutPut>
    {
        private readonly IUsuarioRepository _usurioRepository;
        private readonly IMapper<TDTO, UsuarioEntity> _mapper;
        private readonly IPresenterResponse<int, TOutPut> _presenterResponse;
        private readonly ITimeZoneInfoProvider _tzProvider;

        public AddUsuarioUseCase(IUsuarioRepository UsuarioEntity, IPresenterResponse<int, TOutPut> presenterResponse, IMapper<TDTO, UsuarioEntity> mapper, ITimeZoneInfoProvider tzProvider)
        {
            _usurioRepository = UsuarioEntity;
            _presenterResponse = presenterResponse;
            _tzProvider = tzProvider;
            _mapper = mapper;
        }
        public async Task<IResult> ExecuteAsync(TDTO entity)
        {
            try
            {
                var usuario = _mapper.ToEntity(entity);
                usuario.dtFechaRegistro = _tzProvider.GetCurrentTimeInZone();
                int code = await _usurioRepository.AddAsync(usuario);
                var response = _presenterResponse.Present(code);

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
