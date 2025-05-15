using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CA_InterfaceAdapters_Models;
using Microsoft.AspNetCore.Http;

namespace CA_ApplicationLayer.Usuarios
{
    public class GetAllPermisosUseCase<TOutput>
    {
        private readonly IPermisoRepository _permisoRepository;
        private readonly ILstPagPresenterResponse<PermisoModel, TOutput> _presenterResponse;

        public GetAllPermisosUseCase(IPermisoRepository permisoRepository, ILstPagPresenterResponse<PermisoModel, TOutput> presenterResponse)
        {
            _permisoRepository = permisoRepository;
            _presenterResponse = presenterResponse;
        }

        public async Task<IResult> ExecuteAsync(int pageIndex, int pageSize, string? paramSearch)
        {
            try
            {
                var Permisos = await _permisoRepository.GetAllAsyncPagination(pageIndex, pageSize, paramSearch);
                var response = _presenterResponse.Present(Permisos);

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
