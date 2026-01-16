namespace GadgetCentralService.Models
{
    public class OrderRequest
    {
        public string ProductId { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}