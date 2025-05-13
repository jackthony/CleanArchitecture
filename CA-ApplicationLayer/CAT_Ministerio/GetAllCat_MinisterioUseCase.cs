using CA_ApplicationLayer.CAT_Ministerio;
using CA_ApplicationLayer.EMP_Empresa;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Departamentos
{
    public class GetAllCat_MinisterioUseCase<TEntity, TOutput>
    {
        private readonly ICat_MinisterioRepository<TEntity> _catMinisterioRepository;
        private readonly ILstPresenterResponse<TEntity, TOutput> _lstPresenterResponse;

        public GetAllCat_MinisterioUseCase(ICat_MinisterioRepository<TEntity> catMinisterioRepository, ILstPresenterResponse<TEntity, TOutput> lstPresenterResponse)
        {
            _catMinisterioRepository = catMinisterioRepository;
            _lstPresenterResponse = lstPresenterResponse;
        }

        public async Task<IResult> ExecuteAsync()
        {
            try
            {
                var ministerio = await _catMinisterioRepository.GetAllAsync();
                var response = _lstPresenterResponse.Present(ministerio);

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
