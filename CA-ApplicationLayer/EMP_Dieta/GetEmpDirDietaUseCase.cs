using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CA_InterfaceAdapters_Models;
using Microsoft.AspNetCore.Http;

namespace CA_ApplicationLayer.EMP_Empresa
{
    public class GetEmpDirDietaUseCase<TOutput>
    {
        private readonly IEmp_DietaRepository _empDietaRepository;
        private readonly IPresenterResponse<EMP_DietaModel, TOutput> _presenterResponse;

        public GetEmpDirDietaUseCase(IEmp_DietaRepository empDietaRepository, IPresenterResponse<EMP_DietaModel, TOutput> presenterResponse)
        {
            _empDietaRepository = empDietaRepository;
            _presenterResponse = presenterResponse;
        }

        public async Task<IResult> ExecuteAsync(string sRuc, int cargo)
        {
            try
            {
                var dieta = await _empDietaRepository.GetByRucAndCargo(sRuc, cargo);
                var response = _presenterResponse.Present(dieta);

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
