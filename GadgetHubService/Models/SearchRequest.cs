namespace GadgetHubService.Models
{
    public class SearchRequest
    {
        public string ProductId { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}