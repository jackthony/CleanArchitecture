using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.Dtos.Requests.BeerModule
{
    public class AddBeerRequestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Style { get; set; }
        public decimal Alcohol { get; set; }
    }
}
