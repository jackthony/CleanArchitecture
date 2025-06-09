using Microsoft.AspNetCore.Mvc;

namespace CA_FrameworksDrivers_API.Controllers
{
    public class BeerController : ControllerBase
    {
        public BeerController() { }
        
        [HttpGet]
        [Route("api/beers")]
        public IActionResult GetBeers()
        {
            // This is a placeholder for the actual implementation.
            // In a real application, you would retrieve beers from a database or service.
            var beers = new List<string> { "IPA", "Stout", "Lager" };
            return Ok(beers);
        }

    }
}
