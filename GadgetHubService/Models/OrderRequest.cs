namespace GadgetHubService.Models
{
    public class OrderRequest
    {
        public string Distributor { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}