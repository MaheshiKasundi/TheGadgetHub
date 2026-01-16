using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechWorldService.Data;
using TechWorldService.Models;

namespace TechWorldService.Controllers;

[Route("api/[controller]")]
[ApiController]

public class QuotationController(AppDbContext context) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<QuotationResponse>> GetQuotation(OrderRequest request)
    {
        // FirstOrDefaultAsync finds the product in TechWorldDB
        var product = await context.Products
            .FirstOrDefaultAsync(p => p.ProductId == request.ProductId);

        if (product == null)
        {
            return NotFound("Product not found in TechWorld inventory.");
        }

        return Ok(new QuotationResponse
        {
            PricePerUnit = product.PricePerUnit,
            AvailableQuantity = product.AvailableQuantity,
            DeliveryDays = product.DeliveryDays
        });
    }
}