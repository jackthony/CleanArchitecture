using CA_ApplicationLayer.Common.IMappersFactory;
using CA_EntrerpriseLayer.BeerModule;
using CA_InterfaceAdapters_Mappers.Dtos.Requests.BeerModule;

namespace CA_InterfaceAdapters_Mappers.Mappers.BeerModule
{
    public class BeerMapper : IMapperDtoToOutput<BeerRequestDTO, Beer>
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
