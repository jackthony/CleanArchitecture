using CA_ApplicationLayer.EMP_Empresa;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Usuarios
{
    public class GetAllUsuariosUseCase<Tentity, TOutput>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper<ItemsPaginatorEntity<UsuarioModel>, ItemsPaginatorEntity<Tentity>> _mapper;
        private readonly ILstPagPresenterResponse<Tentity, TOutput> _presenterResponse;

        public GetAllUsuariosUseCase(IUsuarioRepository usuarioRepository, ILstPagPresenterResponse<Tentity, TOutput> presenterResponse, IMapper<ItemsPaginatorEntity<UsuarioModel>, ItemsPaginatorEntity<Tentity>> mapper)
        {
            _usuarioRepository = usuarioRepository;
            _presenterResponse = presenterResponse;
            _mapper = mapper;
        }

        public async Task<IResult> ExecuteAsync(int pageIndex, int pageSize, string? paramSearch)
        {
            try
            {
                var usuario = await _usuarioRepository.GetAllAsyncPagination(pageIndex, pageSize, paramSearch);
                var userDto = _mapper.ToEntity(usuario);
                var response = _presenterResponse.Present(userDto);

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
