using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Models;

namespace CA_ApplicationLayer.EMP_Empresa
{
    public interface IEmp_DietaRepository
    {
        Task<EMP_DietaModel> GetByRucAndCargo(string ruc, int cargo);
    }
}
