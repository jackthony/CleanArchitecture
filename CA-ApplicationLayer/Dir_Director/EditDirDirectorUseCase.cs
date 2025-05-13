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
    public class EditDirDirectorUseCase<TDTO, TOutPut>
    {
        private readonly IDir_DirectorRepository _dir_DirectorRepository;
        private readonly IMapper<TDTO, Dir_DirectorEntity> _mapper;
        private readonly ITimeZoneInfoProvider _tzProvider;
        private readonly IPresenterResponse<bool, TOutPut> _presenterResponse;

        public EditDirDirectorUseCase(IDir_DirectorRepository dir_DirectorRepository, IPresenterResponse<bool, TOutPut> presenterResponse, IMapper<TDTO, Dir_DirectorEntity> mapper, ITimeZoneInfoProvider tzProvider)
        {
            _dir_DirectorRepository = dir_DirectorRepository;
            _presenterResponse = presenterResponse;
            _tzProvider = tzProvider;
            _mapper = mapper;
        }

        public async Task<IResult> ExecuteAsync(TDTO entity)
        {
            try
            {
                var directorio = _mapper.ToEntity(entity);
                directorio.dtFechaModificacion = _tzProvider.GetCurrentTimeInZone();
                bool exit = await _dir_DirectorRepository.UpdateAsync(directorio);
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
