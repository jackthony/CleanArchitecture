using CA_ApplicationLayer.EMP_Empresa;
using CA_ApplicationLayer.ExceptionLog;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Nodos
{
    public class GetNodosUseCase
    {
        private readonly INodoRepository _nodoRepository;

        public GetNodosUseCase(INodoRepository nodoRepository)
        {
            _nodoRepository = nodoRepository;
        }

        public async Task<IResult> ExecuteAsync(int idEmpresa, Guid? idCarpetaPadre = null)
        {
            try
            {
                var empresas = await _nodoRepository.GetByEnterpriseAsync(idEmpresa, idCarpetaPadre);

                return Results.Ok(empresas);
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
