using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.EMP_Empresa
{
    public class GetByIdEmpEmpresaUseCase<TOutput>
    {
        private readonly IEmp_EmpresaRepository _empEmpresaRepository;
        private readonly IPresenterResponse<EMP_EmpresaModel, TOutput> _presenterResponse;

        public GetByIdEmpEmpresaUseCase(IEmp_EmpresaRepository empEmpresaRepository, IPresenterResponse<EMP_EmpresaModel, TOutput> presenterResponse)
        {
            _empEmpresaRepository = empEmpresaRepository;
            _presenterResponse = presenterResponse;
        }

        public async Task<IResult> ExecuteAsync(int nIdMinisterio)
        {
            try
            {
                var empresas = await _empEmpresaRepository.GetById(nIdMinisterio);
                var response = _presenterResponse.Present(empresas);

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
