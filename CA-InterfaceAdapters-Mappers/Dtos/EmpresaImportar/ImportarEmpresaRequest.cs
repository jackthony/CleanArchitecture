using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.Dtos.EmpresaImportar
{
    public class ImportarEmpresaRequest
    {
        public IFormFile Archivo { get; set; } = default!;
        public int UsuarioId { get; set; }
    }
}
