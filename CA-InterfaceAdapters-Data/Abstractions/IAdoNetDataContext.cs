using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CA_InterfaceAdapters_Data.Abstractions
{
    public interface IAdoNetDataContext :IDataContext
    {
        IDbConnection Connection { get; }
        ICommand CreateCommand();
    }
}
