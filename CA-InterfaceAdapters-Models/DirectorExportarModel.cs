using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Models
{
    public class DirectorExportarModel
    {
        public int IdRegistro { get; set; }
        public int IdEmpresa { get; set; }
        public string TipoDocumento { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public string Departamento { get; set; } = string.Empty;
        public string Provincia { get; set; } = string.Empty;
        public string Distrito { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public DateTime? FechaNacimiento { get; set; }
        public string Genero { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
        public string TipoDirector { get; set; } = string.Empty;
        public string Sector { get; set; } = string.Empty;
        public string Profesion { get; set; } = string.Empty;
        public decimal? Dieta { get; set; }
        public string Especialidad { get; set; } = string.Empty;
        public DateTime? FechaNombramiento { get; set; }
        public DateTime? FechaDesignacion { get; set; }
        public string FechaRenuncia { get; set; } = string.Empty;
        public string Comentarios { get; set; } = string.Empty;
    }
}
