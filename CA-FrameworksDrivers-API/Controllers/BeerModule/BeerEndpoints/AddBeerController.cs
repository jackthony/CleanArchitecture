using System.Net;
using CA_ApplicationLayer.UseCases.BeersUseCases;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Mappers.Dtos.Requests.BeerModule;
using CA_InterfaceAdapters_Presenters;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ROP;
using ROP.APIExtensions;

namespace CA_FrameworksDrivers_API.Controllers.BeerModule.BeerEndpoints
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddBeerController : ControllerBase
    {
        private readonly AddBeerUseCase<AddBeerRequestDTO> _addBeerUseCase;
        private readonly IValidator<AddBeerRequestDTO> _validator;

        public AddBeerController(AddBeerUseCase<AddBeerRequestDTO> addBeerUseCase, IValidator<AddBeerRequestDTO> validator)
        {
            _addBeerUseCase = addBeerUseCase;
            _validator = validator;
        }

        [HttpPost]
        [Route("api/addBeer")]
        public async Task<IActionResult> PostAsync([FromBody] AddBeerRequestDTO beerRequest)
        {
            var validation = await _validator.ValidateAsync(beerRequest);

            if (!validation.IsValid)
            {
                var modelState = new ModelStateDictionary();
                foreach (var error in validation.Errors)
                {
                    modelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return ValidationProblem(modelState);
            }

            var result = await _addBeerUseCase.ExecuteAsync(beerRequest);
            return result.ToActionResult();
        }

    }
}
