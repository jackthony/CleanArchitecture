using CA_ApplicationLayer.EMP_Empresa;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Usuarios
{
    public class DeleteUsuarioUseCase<TOutPut>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPresenterResponse<bool, TOutPut> _presenterResponse;

        public DeleteUsuarioUseCase(IUsuarioRepository usuarioRepository, IPresenterResponse<bool, TOutPut> presenterResponse)
        {
            _usuarioRepository = usuarioRepository;
            _presenterResponse = presenterResponse;
        }

        public async Task<IResult> ExecuteAsync(int idEmpresa)
        {
            try
            {
                bool exit = await _usuarioRepository.DeleteAsync(idEmpresa);
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
