using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Nodos
{
    public interface INodoRepository : IRepository<NodoEntity, NodoModel>
    {
        public Task<IEnumerable<NodoModel>> GetByEnterpriseAsync(int idEmpresa, Guid? idCarpetaPadre = null);
    }
}
