using CA_ApplicationLayer.EMP_Empresa;
using CA_EntrerpriseLayer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Usuarios
{
    public class EditUsuarioUseCase<TDTO, TOutPut>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper<TDTO, UsuarioEntity> _mapper;
        private readonly ITimeZoneInfoProvider _tzProvider;
        private readonly IPresenterResponse<bool, TOutPut> _presenterResponse;

        public EditUsuarioUseCase(IUsuarioRepository usuarioRepository, IPresenterResponse<bool, TOutPut> presenterResponse, IMapper<TDTO, UsuarioEntity> mapper, ITimeZoneInfoProvider tzProvider)
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
                var empresa = _mapper.ToEntity(entity);
                empresa.dtFechaModificacion = _tzProvider.GetCurrentTimeInZone();
                bool exit = await _usuarioRepository.UpdateAsync(empresa);
                var response = _presenterResponse.Present(exit);

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
