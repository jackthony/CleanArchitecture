using CA_ApplicationLayer;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Mappers.Dtos.Requests;

namespace CA_InterfaceAdapters_Mappers
{
    public class BeerMapper : IMapper<BeerRequestDTO, Beer>
    {
        public Beer ToEntity(BeerRequestDTO dto)
            => new Beer()
            {
                Id = dto.Id,
                Name = dto.Name,
                Style = dto.Style,
                Alcohol = dto.Alcohol
            };
    }
}
