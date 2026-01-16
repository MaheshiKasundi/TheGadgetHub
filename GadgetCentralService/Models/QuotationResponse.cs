namespace GadgetCentralService.Models
{
    public class QuotationResponse
    {
        public string DistributorName { get; set; } = "GadgetCentral";
        public decimal PricePerUnit { get; set; }
        public int AvailableQuantity { get; set; }
        public int DeliveryDays { get; set; }
    }
}
