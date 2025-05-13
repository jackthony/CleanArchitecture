using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Dir_Director
{
    public interface IDir_DirectorRepository : IRepositoryWrite<Dir_DirectorEntity>
    {
        Task<ItemsPaginatorEntity<Dir_DirectorModel>> GetAllAsyncPaginationByEmpresa(int pageIndex, int pageSize, int nIdEmpresa);
    }
}
