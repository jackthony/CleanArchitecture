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
    public class AddEmpEmpresaUseCase<TDTO, TOutPut>
    {
        private readonly IEmp_EmpresaRepository _empEmpresaRepository;
        private readonly IMapper<TDTO, EMP_EmpresaEntity> _mapper;
        private readonly IPresenterResponse<int, TOutPut> _presenterResponse;
        private readonly ITimeZoneInfoProvider _tzProvider;


        public AddEmpEmpresaUseCase(IEmp_EmpresaRepository empEmpresaRepository, IPresenterResponse<int, TOutPut> presenterResponse, IMapper<TDTO, EMP_EmpresaEntity> mapper, ITimeZoneInfoProvider tzProvider)
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
                empresa.dtFechaRegistro = _tzProvider.GetCurrentTimeInZone();

                int code = await _empEmpresaRepository.AddAsync(empresa);

                //TEMPORAL BORRAR
                if (code > 0)
                {
                    string basePath = @"C:\FonafeStorage\Empresa";
                    string folderPath = Path.Combine(basePath, empresa.sRazonSocial);

                    // Verificar y crear carpeta si no existe
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                }
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
