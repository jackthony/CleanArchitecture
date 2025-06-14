using System;
using System.IO;
using CA_ApplicationLayer;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Mappers.Dtos.ArchivoProceso;
using Microsoft.AspNetCore.Http;

namespace CA_InterfaceAdapters_Mappers
{
    /// <summary>
    /// Convierte <see cref="ArchivoProcesoCreateDTO"/> en <see cref="ArchivoProcesoEntity"/>.
    /// Solo mapea los campos que provienen del cliente; 
    /// los datos que dependen del almacenamiento físico (ruta) o del contexto
    /// (usuario, fechas) los completará el Use Case.
    /// </summary>
    public class ArchivoProcesoCreateMapper
        : IMapper<ArchivoProcesoCreateDTO, ArchivoProcesoEntity>
    {
        public ArchivoProcesoEntity ToEntity(ArchivoProcesoCreateDTO dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            // Se extrae la extensión directamente del archivo que sube el usuario
            var extension = Path.GetExtension(dto.Archivo.FileName)?.ToLowerInvariant() ?? string.Empty;

            return new ArchivoProcesoEntity
            {
                nIdEntidadRelacionada = dto.nIdEntidadRelacionada,
                // 👉  La ruta se completará cuando el Use Case lo guarde
                sRutaFisica = string.Empty,
                formFile = dto.Archivo,

                nIdUsuarioCreacion = dto.nUserId > 0 ? dto.nUserId : default,
                sExtension = extension,
                nIdEntidad = dto.nIdEntidad,
                sRuta = dto.sRuta,

                //sDescripcion = dto.sDescripcion,

                // 👉  Se llenará en el Use Case con DateTime.UtcNow
                dtFechaCreacion = default,

                dtFechaModif = null,
                nIdUsuarioModif = null,
                bEliminado = false
            };
        }
    }
}
