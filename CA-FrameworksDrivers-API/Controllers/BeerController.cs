using System.Net;
using CA_ApplicationLayer;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Mappers.Dtos.Requests;
using CA_InterfaceAdapters_Presenters;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ROP;
using ROP.APIExtensions;

namespace CA_FrameworksDrivers_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BeerController : ControllerBase
    {
        private readonly AddBeerUseCase<BeerRequestDTO> _addBeerUseCase;
        private readonly IValidator<BeerRequestDTO> _validator;

        public BeerController(AddBeerUseCase<BeerRequestDTO> addBeerUseCase, IValidator<BeerRequestDTO> validator)
        {
            _addBeerUseCase = addBeerUseCase;
            _validator = validator;
        }

        [HttpPost]
        [Route("api/addBeer")]
        public async Task<IActionResult> PostAsync([FromBody] BeerRequestDTO beerRequest)
        {
            //var validation = await _validator.ValidateAsync(beerRequest);
            //if (!validation.IsValid)
            //{
            //    // Crear un objeto de tipo ValidationProblemDetails
            //    var validationProblemDetails = new ValidationProblemDetails(validation.ToDictionary());

            //    // Opcional: Puedes establecer un estado HTTP 400 (Bad Request) de forma explícita
            //    validationProblemDetails.Status = (int)HttpStatusCode.BadRequest;

            //    return ValidationProblem(validationProblemDetails);
            //}

            //// Si la validación es exitosa, ejecuta el caso de uso
            //return await _addBeerUseCase.ExecuteAsync(beerRequest)
            //    .UseSuccessHttpStatusCode(HttpStatusCode.Created)
            //    .ToActionResult();
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
