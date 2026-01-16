namespace TechWorldService.Models
{
    public class OrderRequest
    {
        // These property names must match the GadgetHub exactly
        public string ProductId { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}