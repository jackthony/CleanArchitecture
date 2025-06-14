using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.EMP_Empresa
{
    public class EditEmpEmpresaUseCase<TDTO, TOutPut>
    {
        private readonly IEmp_EmpresaRepository _empEmpresaRepository;
        private readonly IMapper<TDTO, EMP_EmpresaEntity> _mapper;
        private readonly ITimeZoneInfoProvider _tzProvider;
        private readonly IPresenterResponse<bool, TOutPut> _presenterResponse;

        public EditEmpEmpresaUseCase(IEmp_EmpresaRepository empEmpresaRepository, IPresenterResponse<bool, TOutPut> presenterResponse, IMapper<TDTO, EMP_EmpresaEntity> mapper, ITimeZoneInfoProvider tzProvider)
        {
            _empEmpresaRepository = empEmpresaRepository;
            _presenterResponse= presenterResponse;
            _tzProvider = tzProvider;
            _mapper=mapper;
        }

        public async Task<IResult> ExecuteAsync(TDTO entity)
        {
            try
            {
                var empresa = _mapper.ToEntity(entity);
                empresa.dtFechaModificacion = _tzProvider.GetCurrentTimeInZone();
                bool exit = await _empEmpresaRepository.UpdateAsync(empresa);

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
