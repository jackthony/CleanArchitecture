using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CA_InterfaceAdapters_Models;
using Microsoft.AspNetCore.Http;

namespace CA_ApplicationLayer.Usuarios
{
    public class GetAllRolesUseCase<TOutput>
    {
        private readonly IRolRepository _rolRepository;
        private readonly ILstPagPresenterResponse<RolModel, TOutput> _presenterResponse;

        public GetAllRolesUseCase(IRolRepository rolRepository, ILstPagPresenterResponse<RolModel, TOutput> presenterResponse)
        {
            _rolRepository = rolRepository;
            _presenterResponse = presenterResponse;
        }

        public async Task<IResult> ExecuteAsync(int pageIndex, int pageSize, string? paramSearch)
        {
            try
            {
                var roles = await _rolRepository.GetAllAsyncPagination(pageIndex, pageSize, paramSearch);
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
