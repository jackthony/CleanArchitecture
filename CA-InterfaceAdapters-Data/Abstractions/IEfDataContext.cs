using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CA_InterfaceAdapters_Models;
using Microsoft.EntityFrameworkCore;

namespace CA_InterfaceAdapters_Data.Abstractions
{
    public interface IEfDataContext : IDataContext
    {
        DbSet<BeerModel> Beers { get; }
        DbSet<SaleModel> Sales { get; }
        DbSet<ConceptModel> Concepts { get; }
    }
}
