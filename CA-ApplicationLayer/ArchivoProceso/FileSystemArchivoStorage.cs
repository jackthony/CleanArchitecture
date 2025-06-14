using CA_ApplicationLayer.ArchivoProceso;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

public sealed class FileSystemArchivoStorage : IArchivoStorage
{
    private readonly string _rootPath;   // Ej.:  D:\FonafeStorage

    public FileSystemArchivoStorage(IConfiguration cfg, IWebHostEnvironment env)
    {
        // 1) lee de appsettings.json;  2) si falta, usa wwwroot\storage
        _rootPath = cfg["FileStorage:RootPath"]
                    ?? Path.Combine(env.WebRootPath, "storage");

        Directory.CreateDirectory(_rootPath); // asegura raíz
    }

    public async Task<string> SaveAsync(
        IFormFile file, string folderContent, CancellationToken ct = default)
    {
        if (file.Length == 0)
            throw new InvalidOperationException("Archivo vacío.");

        // Estructura de carpetas:  {Root}\Entidad\{Id}\yyyy\MM\
        var now = DateTime.UtcNow;
        var folder = Path.Combine(
                            _rootPath,
                            folderContent);

        Directory.CreateDirectory(folder);

        // Nombre único (GUID + extensión original)
        var ext = Path.GetExtension(file.FileName);
        var uidName = $"{Guid.NewGuid():N}{ext}";
        var fullPath = Path.Combine(folder, uidName);

        // Copiar al disco
        await using var fs = new FileStream(fullPath, FileMode.CreateNew);
        await file.CopyToAsync(fs, ct);

        // Ruta relativa que irá a la BD  →  "Entidad/1234/2025/05/xxxxxxxx.pdf"
        var relativePath = Path.GetRelativePath(_rootPath, fullPath)
                               .Replace('\\', '/');     // para web-compat

        return relativePath;
    }
}
