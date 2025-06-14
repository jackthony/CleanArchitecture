using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CA_ApplicationLayer.ArchivoProceso
{
    public interface IArchivoStorage
    {
        /// <summary>Guarda el archivo y devuelve la ruta relativa.</summary>
        Task<string> SaveAsync(IFormFile file,
                               string folderContent,
                               CancellationToken ct = default);
    }

    public interface IClock
    {
        DateTime UtcNow { get; }
    }

    public interface ICurrentUserService
    {
        int UserId { get; }
    }


}
