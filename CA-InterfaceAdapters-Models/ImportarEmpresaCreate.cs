using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.Dtos.ExportarEmpresas
{
    public class ImportarEmpresaCreate
    {
        public string Ruc { get; set; } = string.Empty;
        public string RazonSocial { get; set; } = string.Empty;
        public string Departamento { get; set; } = string.Empty;
        public string Provincia { get; set; } = string.Empty;
        public string Distrito { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Rubro { get; set; } = string.Empty;
        public string Proponente { get; set; } = string.Empty;
        public string IngresosUltimoAnio { get; set; } = string.Empty;
        public string UtilidadUltimoAnio { get; set; } = string.Empty;
        public string CapitalSocial { get; set; } = string.Empty;
        public string NumeroMiembros { get; set; } = string.Empty;
        public string RegistroEnMercadoValores { get; set; } = string.Empty;
        public string Activo { get; set; } = string.Empty;
        public string Comentario { get; set; } = string.Empty;
    }
}
