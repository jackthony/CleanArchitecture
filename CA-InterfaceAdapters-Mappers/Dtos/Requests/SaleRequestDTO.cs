using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.Dtos.Requests
{
    public class SaleRequestDTO
    {
        public List<ConceptRequestDTO> Concepts { get; set; }
    }

    public class ConceptRequestDTO
    {
        public int IdBeer { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
