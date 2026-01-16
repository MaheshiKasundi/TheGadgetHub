namespace GadgetHubService.Models
{
    public class DistributorQuote
    {
        public string DistributorName { get; set; } = string.Empty;

        public string ProductId { get; set; } = string.Empty;

        public decimal PricePerUnit { get; set; }

        public int AvailableQuantity { get; set; }

        public int DeliveryDays { get; set; }
    }
}