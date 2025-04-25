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
    public class DeleteEmpEmpresaUseCase<TOutPut>
    {
        private readonly IEmp_EmpresaRepository _empEmpresaRepository;
        private readonly IPresenterResponse<bool, TOutPut> _presenterResponse;

        public DeleteEmpEmpresaUseCase(IEmp_EmpresaRepository empEmpresaRepository, IPresenterResponse<bool, TOutPut> presenterResponse)
        {
            _empEmpresaRepository = empEmpresaRepository;
            _presenterResponse= presenterResponse;
        }

        public async Task<IResult> ExecuteAsync(int idEmpresa)
        {
            try
            {
                bool exit = await _empEmpresaRepository.DeleteAsync(idEmpresa);
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
