using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CA_EntrerpriseLayer;

namespace CA_InterfaceAdapters_Mappers.Dtos.USUARIOS
{
    public class LoginResponseDTO
    {
        public int usuario { get; set; }
        public string email { get; set; } = string.Empty;
        public string nombreCompleto { get; set; } = string.Empty;
        public string primerNombre { get; set; } = string.Empty;
        public string nombreVisual { get; set; } = string.Empty;
        public string sessionState { get; set; } = string.Empty;

        // Nueva propiedad que contiene permisos agrupados por módulo
        public List<PermisosPorRolEntity> permissions { get; set; } = new List<PermisosPorRolEntity>();
    }
}
