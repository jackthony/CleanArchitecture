using CA_ApplicationLayer.Distritos;
using CA_ApplicationLayer.EMP_Empresa;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Distritos
{
    public class GetAllDistritosUseCase<TEntity, TOutput>
    {
        private readonly IDistritosRepository<TEntity> _distritosRepository;
        private readonly ILstPresenterResponse<TEntity, TOutput> _lstPresenterResponse;

        public GetAllDistritosUseCase(IDistritosRepository<TEntity> distritosRepository, ILstPresenterResponse<TEntity, TOutput> lstPresenterResponse)
        {
            _distritosRepository = distritosRepository;
            _lstPresenterResponse = lstPresenterResponse;
        }

        public async Task<IResult> ExecuteAsync(string provinceCode)
        {
            try
            {
                var provinces = await _distritosRepository.GetAllAsync(provinceCode);
                var response = _lstPresenterResponse.Present(provinces);

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
