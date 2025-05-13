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
    public class GetAllProvinciasUseCase<TEntity, TOutput>
    {
        private readonly IProvinciasRepository<TEntity> _provinciasRepository;
        private readonly ILstPresenterResponse<TEntity, TOutput> _lstPresenterResponse;

        public GetAllProvinciasUseCase(IProvinciasRepository<TEntity> provinciasRepository, ILstPresenterResponse<TEntity, TOutput> lstPresenterResponse)
        {
            _provinciasRepository = provinciasRepository;
            _lstPresenterResponse = lstPresenterResponse;
        }

        public async Task<IResult> ExecuteAsync(string departmentCode)
        {
            try
            {
                var provinces = await _provinciasRepository.GetAllAsync(departmentCode);
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
