using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CA_InterfaceAdapters_Models;
using Microsoft.AspNetCore.Http;

namespace CA_ApplicationLayer.Usuarios
{
    public class GetAllProcesosUseCase<TOutput>
    {
        private readonly IProcesoRepository _procesoRepository;
        private readonly ILstPagPresenterResponse<ProcesoModel, TOutput> _presenterResponse;

        public GetAllProcesosUseCase(IProcesoRepository procesoRepository, ILstPagPresenterResponse<ProcesoModel, TOutput> presenterResponse)
        {
            _procesoRepository = procesoRepository;
            _presenterResponse = presenterResponse;
        }

        public async Task<IResult> ExecuteAsync(int pageIndex, int pageSize, string? paramSearch)
        {
            try
            {
                var roles = await _procesoRepository.GetAllAsyncPagination(pageIndex, pageSize, paramSearch);
                var response = _presenterResponse.Present(roles);

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
