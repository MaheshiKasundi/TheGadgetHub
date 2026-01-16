namespace TechWorldService.Models;

public class QuotationResponse
{
    public string DistributorName { get; set; } = "TechWorld";
    public decimal PricePerUnit { get; set; }
    public int AvailableQuantity { get; set; }
    public int DeliveryDays { get; set; }
}