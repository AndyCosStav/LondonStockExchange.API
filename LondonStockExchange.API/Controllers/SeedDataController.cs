using LondonStockExchange.BusinessLogic.Services.SeedData;
using Microsoft.AspNetCore.Mvc;

namespace LondonStockExchange.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedDataController : ControllerBase
    {
        private readonly ISeedDataService _seedDataService;

        public SeedDataController(ISeedDataService seedDataService)
        {
            _seedDataService = seedDataService;
        }

        [HttpPost]
        public IActionResult SeedStockData()
        {
            _seedDataService.SeedStockData();
            return Ok("Database seeded with sample stock data.");
        }

    }

}