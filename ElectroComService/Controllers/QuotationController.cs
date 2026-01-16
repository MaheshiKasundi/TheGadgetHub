using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectroComService.Data;
using ElectroComService.Models;

namespace ElectroComService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuotationController(AppDbContext context) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<QuotationResponse>> GetQuotation(OrderRequest request)
    {
        var product = await context.Products
            .FirstOrDefaultAsync(p => p.ProductId == request.ProductId);

        if (product == null)
        {
            return NotFound("Product not found in ElectroCom inventory.");
        }

        return Ok(new QuotationResponse
        {
            PricePerUnit = product.PricePerUnit,
            AvailableQuantity = product.AvailableQuantity,
            DeliveryDays = product.DeliveryDays
        });
    }
}