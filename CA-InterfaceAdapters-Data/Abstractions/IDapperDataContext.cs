using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Data.Abstractions
{
    public interface IDapperDataContext : IDataContext
    {
        IDbConnection Connection { get; }
        IDbTransaction BeginTransaction();
    }
}
