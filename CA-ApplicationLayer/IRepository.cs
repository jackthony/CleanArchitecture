using CA_EntrerpriseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer
{

    public interface IRepository<TTEntity, TModel> : IRepositoryRead<TModel>, IRepositoryWrite<TTEntity>
    {
    }
}
