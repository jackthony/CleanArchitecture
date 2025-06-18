using CA_ApplicationLayer;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Mappers.Dtos.EMP_EMPRESA;
using CA_InterfaceAdapters_Mappers.Dtos.EmpresaImportar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.EmpresaImportar
{
    public class ImportarEmpresaCreateMapper : IMapper<ImportarEmpresaRequest, ImportarEmpresasEntity>
    {
        public ImportarEmpresasEntity ToEntity(ImportarEmpresaRequest dto)
            => new()
            {
                Archivo = dto.Archivo,
                UsuarioId = dto.UsuarioId,
            };
    }
}
