using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.Dtos.ExportarEmpresas
{
    public class ImportarDirectorCreate
    {
        public string Ruc { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Departamento { get; set; } = string.Empty;
        public string Provincia { get; set; } = string.Empty;
        public string Distrito { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string FechaNacimiento { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
        public string TipoDirector { get; set; } = string.Empty;
        public string Sector { get; set; } = string.Empty;
        public string Profesion { get; set; } = string.Empty;
        public string Dieta { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        public string FechaNombramiento { get; set; } = string.Empty;
        public string FechaDesignacion { get; set; } = string.Empty;
        public string FechaRenuncia { get; set; } = string.Empty;
        public string Comentarios { get; set; } = string.Empty;
    }
}
