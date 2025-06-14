using CA_ApplicationLayer.EMP_Empresa;
using CA_EntrerpriseLayer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Dir_Director
{
    public class AddDirDirectorUseCase<TDTO, TOutPut>
    {
        private readonly IEmp_EmpresaRepository _empEmpresaRepository;
        private readonly IDir_DirectorRepository _dir_DirectorRepository;
        private readonly IMapper<TDTO, Dir_DirectorEntity> _mapper;
        private readonly IPresenterResponse<int, TOutPut> _presenterResponse;
        private readonly ITimeZoneInfoProvider _tzProvider;


        public AddDirDirectorUseCase(IDir_DirectorRepository dir_DirectorRepository, IPresenterResponse<int, TOutPut> presenterResponse, IMapper<TDTO, Dir_DirectorEntity> mapper, ITimeZoneInfoProvider tzProvider, IEmp_EmpresaRepository empEmpresaRepository)
        {
            _dir_DirectorRepository = dir_DirectorRepository;
            _presenterResponse = presenterResponse;
            _tzProvider = tzProvider;
            _mapper = mapper;
            _empEmpresaRepository = empEmpresaRepository;
        }

        public async Task<IResult> ExecuteAsync(TDTO entity)
        {
            try
            {
                var directorio = _mapper.ToEntity(entity);
                directorio.dtFechaRegistro = _tzProvider.GetCurrentTimeInZone();
                int code = await _dir_DirectorRepository.AddAsync(directorio);

                var empresa = await _empEmpresaRepository.GetById(directorio.nIdEmpresa);

                if (code > 0)
                {
                    string basePath = Path.Combine(@"C:\FonafeStorage\Empresa\"+empresa.sRazonSocial, directorio.sNumeroDocumento);

                    // Lista con los nombres de las carpetas a crear
                    var carpetas = new List<string>
                        {
                            "Propuesta",
                            "Pre - Evaluación",
                            "Evaluación",
                            "Designación"
                        };

                    foreach (var carpeta in carpetas)
                    {
                        string rutaCarpeta = Path.Combine(basePath, carpeta);

                        if (!Directory.Exists(rutaCarpeta))
                        {
                            Directory.CreateDirectory(rutaCarpeta);
                        }
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
