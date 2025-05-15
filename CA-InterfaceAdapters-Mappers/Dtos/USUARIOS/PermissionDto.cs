using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.Dtos.USUARIOS
{
    public class PermissionDto
    {
        public string Module { get; set; } = string.Empty;
        public List<string> Actions { get; set; } = new List<string>();
    }
}
