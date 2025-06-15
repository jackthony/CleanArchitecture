using CA_InterfaceAdapters_Mappers.Dtos.Requests.BeerModule;
using FluentValidation;

namespace CA_FrameworksDrivers_API.Validators.BeerModule
{
    public class BeerValidator : AbstractValidator<BeerRequestDTO>
    {
        public BeerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("La cerveza debe tener un nombre");
            RuleFor(x => x.Style).NotEmpty().WithMessage("Style is required.");
            RuleFor(x => x.Alcohol).GreaterThan(0).WithMessage("La cerveza debe tener alcohol");
        }
    }
}
