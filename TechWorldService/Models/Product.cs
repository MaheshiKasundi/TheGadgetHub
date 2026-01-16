using System.ComponentModel.DataAnnotations.Schema;

namespace TechWorldService.Models;

public class Product
{
    public int Id { get; set; } // Primary Key for SQL
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal PricePerUnit { get; set; }
    public int AvailableQuantity { get; set; }
    public int DeliveryDays { get; set; }
}