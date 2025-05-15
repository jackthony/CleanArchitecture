using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_EntrerpriseLayer
{
    public class PermisosPorRolEntity
    {
        public string Module { get; set; } = string.Empty;
        public List<string> Actions { get; set; } = new List<string>();
    }
}
