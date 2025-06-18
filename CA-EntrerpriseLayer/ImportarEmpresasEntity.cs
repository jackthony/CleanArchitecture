using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_EntrerpriseLayer
{
    public class ImportarEmpresasEntity
    {
        public IFormFile Archivo { get; set; } = default!;
        public int UsuarioId { get; set; }
    }
}
