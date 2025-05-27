using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CA_InterfaceAdapters_Mappers.Dtos.ArchivoProceso
{
    public class ArchivoProcesoCreateDTO
    {
        [Required(ErrorMessage = "El archivo es obligatorio.")]
        public IFormFile Archivo { get; set; } = null!;

        [Required(ErrorMessage = "El ID del tipo de entidad relacionada es obligatorio.")]
        public int nIdEntidadRelacionada { get; set; }

        public int nUserId { get; set; } = 0; // Este campo puede ser opcional, dependiendo de la implementación del Use Case

        public int nIdEntidad { get; set; }
    }
}
