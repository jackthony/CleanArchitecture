using CA_ApplicationLayer.EMP_Empresa;
using CA_InterfaceAdapters_Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Usuarios
{
    public class GetAllUsuariosUseCase<TOutput>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILstPagPresenterResponse<UsuarioModel, TOutput> _presenterResponse;

        public GetAllUsuariosUseCase(IUsuarioRepository usuarioRepository, ILstPagPresenterResponse<UsuarioModel, TOutput> presenterResponse)
        {
            _usuarioRepository = usuarioRepository;
            _presenterResponse = presenterResponse;
        }

        public async Task<IResult> ExecuteAsync(int pageIndex, int pageSize, string? paramSearch)
        {
            try
            {
                var usuario = await _usuarioRepository.GetAllAsyncPagination(pageIndex, pageSize, paramSearch);
                var response = _presenterResponse.Present(usuario);

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
