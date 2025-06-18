using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Mappers.Dtos.ExportarEmpresas;
using CA_InterfaceAdapters_Models;
using DocumentFormat.OpenXml.EMMA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.EMP_Empresa
{
    public interface IEmp_EmpresaRepository : IRepository<EMP_EmpresaEntity, EMP_EmpresaModel>
    {
        Task<IEnumerable<EmpresaExportarModel>> GetAllEmpresasExportarAsync();
        Task<bool> UploadEmpresasAsync(List<ImportarEmpresaCreate> empresas, List<ImportarDirectorCreate> directores, int usuarioId);
    }
}
