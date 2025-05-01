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
    public class GetAllDepartamentosUseCase<TEntity, TOutput>
    {
        private readonly IDepartamentosRepository<TEntity> _departamentosRepository;
        private readonly ILstPresenterResponse<TEntity, TOutput> _lstPresenterResponse;

        public GetAllDepartamentosUseCase(IDepartamentosRepository<TEntity> departamentosRepository, ILstPresenterResponse<TEntity, TOutput> lstPresenterResponse)
        {
            _departamentosRepository = departamentosRepository;
            _lstPresenterResponse = lstPresenterResponse;
        }

        public async Task<IResult> ExecuteAsync()
        {
            try
            {
                var departaments = await _departamentosRepository.GetAllAsync();
                var response = _lstPresenterResponse.Present(departaments);

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
