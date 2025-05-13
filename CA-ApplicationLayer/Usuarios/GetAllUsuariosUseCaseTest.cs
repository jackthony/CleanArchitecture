//using CA_ApplicationLayer.Provincias;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CA_ApplicationLayer.Usuarios
//{
//    public class GetAllUsuariosUseCase<TEntity, TOutput>
//    {
//        private readonly IUsuarioRepository<TEntity> _repository;
//        private readonly ILstPresenterResponse<TEntity, TOutput> _lstPresenterResponse;

//        public GetAllUsuariosUseCase(IUsuarioRepository<TEntity> repository, ILstPresenterResponse<TEntity, TOutput> lstPresenterResponse)
//        {
//            _repository = repository;
//            _lstPresenterResponse = lstPresenterResponse;
//        }
//        public async Task<TOutput> ExecuteAsync(string nombreUsuario)
//        {

//                var usuarios = await _repository.GetAllAsync(nombreUsuario);
//                var response = _lstPresenterResponse.Present(usuarios);
//                return response;
//        }
//    }
//}
