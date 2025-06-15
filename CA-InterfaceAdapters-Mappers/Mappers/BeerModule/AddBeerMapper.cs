using CA_ApplicationLayer.Common.IMappersFactory;
using CA_EntrerpriseLayer.BeerModule;
using CA_InterfaceAdapters_Mappers.Dtos.Requests.BeerModule;

namespace CA_InterfaceAdapters_Mappers.Mappers.BeerModule
{
    public class AddBeerMapper : IMapperDtoToOutput<AddBeerRequestDTO, BeerEntity>
    {
        public BeerEntity ToEntity(AddBeerRequestDTO dto)
            => new BeerEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                Style = dto.Style,
                Alcohol = dto.Alcohol
            };
    }
}
