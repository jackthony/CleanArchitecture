using CA_ApplicationLayer.Usuarios;
using CA_EntrerpriseLayer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.ExceptionLog
{
    public class AddExeptionLogUseCase<TDTO, TOutPut>
    {
        private readonly IExeptionLogRepository _exeptionLogRepository;
        private readonly IMapper<TDTO, ExeptionLogEntity> _mapper;
        private readonly IPresenterResponse<int, TOutPut> _presenterResponse;
        private readonly ITimeZoneInfoProvider _tzProvider;

        public AddExeptionLogUseCase(IExeptionLogRepository exeptionLogRepository, IPresenterResponse<int, TOutPut> presenterResponse, IMapper<TDTO, ExeptionLogEntity> mapper, ITimeZoneInfoProvider tzProvider)
        {
            _exeptionLogRepository = exeptionLogRepository;
            _presenterResponse = presenterResponse;
            _tzProvider = tzProvider;
            _mapper = mapper;
        }
        public async Task<IResult> ExecuteAsync(TDTO entity)
        {
            try
            {
                var exeptionLog = _mapper.ToEntity(entity);
                exeptionLog.FechaRegistro = _tzProvider.GetCurrentTimeInZone();
                int code = await _exeptionLogRepository.AddAsync(exeptionLog);
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
