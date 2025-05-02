using CA_ApplicationLayer.EMP_Empresa;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Provincias
{
    public class GetAllConstanteUseCase<TEntity, TOutput>
    {
        private readonly IConstanteRepository<TEntity> _constanteRepository;
        private readonly ILstPresenterResponse<TEntity, TOutput> _lstPresenterResponse;

        public GetAllConstanteUseCase(IConstanteRepository<TEntity> constanteRepository, ILstPresenterResponse<TEntity, TOutput> lstPresenterResponse)
        {
            _constanteRepository = constanteRepository;
            _lstPresenterResponse = lstPresenterResponse;
        }

        public async Task<IResult> ExecuteAsync(int nConCodigo)
        {
            try
            {
                var provinces = await _constanteRepository.GetAllAsync(nConCodigo);
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
