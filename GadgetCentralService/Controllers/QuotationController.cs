using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GadgetCentralService.Data;
using GadgetCentralService.Models;

namespace GadgetCentralService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuotationController : ControllerBase
{
    private readonly AppDbContext _context;
    public QuotationController(AppDbContext context) { _context = context; }

    [HttpPost]
    public async Task<ActionResult<QuotationResponse>> GetQuotation(OrderRequest request)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.ProductId == request.ProductId);

        if (product == null) return NotFound("Product not found at GadgetCentral.");

        return Ok(new QuotationResponse
        {
            PricePerUnit = product.PricePerUnit,
            AvailableQuantity = product.AvailableQuantity,
            DeliveryDays = product.DeliveryDays
        });
    }
}