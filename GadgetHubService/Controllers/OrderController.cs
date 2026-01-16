using Microsoft.AspNetCore.Mvc;
using GadgetHubService.Models;
using GadgetHubService.Services;

namespace GadgetHubService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly DistributorClientService _client;

      private readonly Dictionary<string, string> _distributors = new(StringComparer.OrdinalIgnoreCase)
{
    { "TechWorld", "https://localhost:44307" },
    { "ElectroCom", "https://localhost:7030" },
    { "GadgetCentral", "https://localhost:7276" }
};

        public OrderController(DistributorClientService client)
        {
            _client = client;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderRequest req)
        {
            // 1. Find the URL for the chosen distributor
            if (!_distributors.ContainsKey(req.Distributor))
            {
                return BadRequest("Invalid distributor name.");
            }

            string targetUrl = _distributors[req.Distributor];

            // 2. Forward the order using the Service
            bool isSuccess = await _client.PlaceOrderAsync($"{targetUrl}/api/Product/order", req);

            if (isSuccess)
            {
                return Ok(new { Message = $"Order successfully placed with {req.Distributor}!" });
            }

            return StatusCode(500, "The distributor could not process the order at this time.");
        }
    }
}