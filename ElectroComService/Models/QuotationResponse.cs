namespace ElectroComService.Models
{
    public class QuotationResponse
    {
        public string DistributorName { get; set; } = "ElectroCom"; // Set for this service
        public decimal PricePerUnit { get; set; }
        public int AvailableQuantity { get; set; }
        public int DeliveryDays { get; set; }
    }
}
