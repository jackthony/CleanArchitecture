using CA_EntrerpriseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Provincias
{
    public interface IConstanteRepository<TOutput>
    {
        Task<IEnumerable<TOutput>> GetAllAsync(int nConCodigo);
    }
}
