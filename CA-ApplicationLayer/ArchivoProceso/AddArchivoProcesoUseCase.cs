using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using CA_EntrerpriseLayer;

namespace CA_ApplicationLayer.ArchivoProceso
{
    /// <summary>
    /// Registra un archivo en el File-System y persiste sus metadatos.
    /// </summary>
    public sealed class AddArchivoProcesoUseCase<TDTO, TOutput>
    {
        private readonly IArchivoProcesoRepository _repository;
        private readonly IMapper<TDTO, ArchivoProcesoEntity> _mapper;
        private readonly IPresenterResponse<int, TOutput> _presenter;
        private readonly IArchivoStorage _storage;
        private readonly IClock _clock;

        public AddArchivoProcesoUseCase(
            IArchivoProcesoRepository repository,
            IMapper<TDTO, ArchivoProcesoEntity> mapper,
            IPresenterResponse<int, TOutput> presenter,
            IArchivoStorage storage,
            IClock clock)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
        }

        /// <summary>
        /// Ejecuta el caso de uso y devuelve un <see cref="IResult"/> propio de Minimal API.
        /// </summary>
        public async Task<IResult> ExecuteAsync(TDTO dto, CancellationToken ct = default)
        {
            // ───────────────────────────────────────────────
            // 1. Validar entrada básica
            // ───────────────────────────────────────────────
            if (dto == null)
                return TypedResults.BadRequest(new { message = "El cuerpo de la petición es nulo." });

            

            // ───────────────────────────────────────────────
            // 2. Mapear DTO → Entidad (sin datos de storage aún)
            // ───────────────────────────────────────────────
            var entity = _mapper.ToEntity(dto);

            if (entity.formFile == null || entity.formFile.Length == 0)
                return TypedResults.BadRequest(new { message = "El DTO no contiene un archivo." });

            // ───────────────────────────────────────────────
            // 3. Guardar físicamente
            //    (el servicio devuelve la ruta relativa o url)
            // ───────────────────────────────────────────────
            entity.sRutaFisica = await _storage.SaveAsync(
                                        entity.formFile,
                                        entity.nIdEntidadRelacionada,
                                        ct);

            entity.dtFechaCreacion = _clock.UtcNow;
            entity.nIdUsuarioCreacion = entity.nIdUsuarioCreacion;
            entity.sNombreFile = entity.formFile.FileName;

            // ───────────────────────────────────────────────
            // 4. Persistir metadatos
            // ───────────────────────────────────────────────
            int newId = await _repository.AddAsync(entity, ct);

            // ───────────────────────────────────────────────
            // 5. Presentar salida de forma consistente
            // ───────────────────────────────────────────────
            var output = _presenter.Present(newId);
            return TypedResults.Ok(output);
        }

        /// <summary>
        /// Esta interfaz mínima permite que el Use Case sea genérico
        /// mientras asegura acceso a <see cref="IFormFile"/>.
        /// El DTO de subida (p.ej. <c>ArchivoProcesoCreateDTO</c>) debe implementarla.
        /// </summary>
        private interface IHasFormFile
        {
            IFormFile Archivo { get; }
            int nIdEntidadRelacionada { get; }
        }
    }
}
