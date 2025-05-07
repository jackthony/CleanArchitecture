using CA_ApplicationLayer.EMP_Empresa;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Dir_Director
{
    public class DeleteDirDirectorUseCase<TOutPut>
    {
        private readonly IDir_DirectorRepository _dirDirectorRepository;
        private readonly IPresenterResponse<bool, TOutPut> _presenterResponse;

        public DeleteDirDirectorUseCase(IDir_DirectorRepository dirDirectorRepository, IPresenterResponse<bool, TOutPut> presenterResponse)
        {
            _dirDirectorRepository = dirDirectorRepository;
            _presenterResponse= presenterResponse;
        }

        public async Task<IResult> ExecuteAsync(int nIdRegistro)
        {
            try
            {
                bool exit = await _dirDirectorRepository.DeleteAsync(nIdRegistro);
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
