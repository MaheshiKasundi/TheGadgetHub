using GadgetHubService.Models;
using GadgetHubService.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly DistributorClientService _client;

    // Define the URLs for your three services (Check your port numbers!)
    // Change your dictionary definition to this:
    private readonly Dictionary<string, string> _distributors = new(StringComparer.OrdinalIgnoreCase)
{
    { "TechWorld", "https://localhost:44307" },
    { "ElectroCom", "https://localhost:7030" },
    { "GadgetCentral", "https://localhost:7276" }
};

    public SearchController(DistributorClientService client)
    {
        _client = client;
    }

    [HttpPost]
    public async Task<ActionResult<BestQuoteResponse>> GetBestDeal([FromBody] SearchRequest req)
    {
        var validQuotes = new List<BestQuoteResponse>();

        foreach (var entry in _distributors)
        {
            var quote = await _client.GetQuoteAsync(entry.Value, req.ProductId, req.Quantity);

            // Rule: Only consider them if they have enough stock
            if (quote != null && quote.AvailableQuantity >= req.Quantity)
            {
                decimal finalPrice = CalculateHubPrice(quote, req.Quantity);

                validQuotes.Add(new BestQuoteResponse(
                    quote.DistributorName,
                    finalPrice,
                    quote.DeliveryDays,
                    "Deal available"
                ));
            }
        }

        if (validQuotes.Count == 0) return NotFound("No distributor has enough stock.");

        // Find the one with the lowest price
        var bestDeal = validQuotes.OrderBy(q => q.FinalTotalPrice).First();
        bestDeal.Message = "Found the best price for you!";

        return Ok(bestDeal);
    }

    private decimal CalculateHubPrice(DistributorQuote q, int qty)
    {
        // 1. Get specific distributor discount
        decimal discount = q.DistributorName switch
        {
            "TechWorld" => 0.05m,
            "ElectroCom" => 0.07m,
            "GadgetCentral" => 0.03m,
            _ => 0m
        };

        decimal basePrice = q.PricePerUnit * qty;
        decimal discountedPrice = basePrice * (1 - discount);

        // 2. Add GadgetHub 5% commission
        return discountedPrice * 1.05m;
    }
}