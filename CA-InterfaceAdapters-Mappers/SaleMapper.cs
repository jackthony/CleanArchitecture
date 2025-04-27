using CA_ApplicationLayer;
using CA_EntrerpriseLayer.venta;
using CA_InterfaceAdapters_Mappers.Dtos.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers
{
    public class SaleMapper : IMapper<SaleRequestDTO, Sale>
    {
        public Sale ToEntity(SaleRequestDTO dto)
        {
            var total = dto.Concepts.Sum(c => c.UnitPrice * c.Quantity);
            var concepts = new List<Concept>();

            foreach (var concept in dto.Concepts)
            {
                concepts.Add(new Concept(concept.IdBeer, concept.Quantity, concept.UnitPrice));
            }

            return new Sale(DateTime.Now, concepts);

        }
    }
}
