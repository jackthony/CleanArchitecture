using CA_ApplicationLayer.EMP_Empresa;
using CA_InterfaceAdapters_Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Dir_Director
{
    public class GetAllDirDirectorUseCase<TOutput>
    {
        private readonly IDir_DirectorRepository _dirDirectorRepository;
        private readonly ILstPagPresenterResponse<Dir_DirectorModel, TOutput> _presenterResponse;

        public GetAllDirDirectorUseCase(IDir_DirectorRepository dirDirectorRepository, ILstPagPresenterResponse<Dir_DirectorModel, TOutput> presenterResponse)
        {
            _dirDirectorRepository = dirDirectorRepository;
            _presenterResponse = presenterResponse;
        }

        public async Task<IResult> ExecuteAsync(int pageIndex, int pageSize, int nIdEmpresa)
        {
            try
            {
                var directorios = await _dirDirectorRepository.GetAllAsyncPaginationByEmpresa(pageIndex, pageSize, nIdEmpresa);
                var response = _presenterResponse.Present(directorios);

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
