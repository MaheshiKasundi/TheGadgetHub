namespace GadgetHubService.Models
{
    public class BestQuoteResponse
    {
        public string BestDistributor { get; set; } = string.Empty;

        public decimal FinalTotalPrice { get; set; }

        public int DeliveryDays { get; set; }

        public string Message { get; set; } = string.Empty;

        public BestQuoteResponse(string bestDistributor, decimal finalTotalPrice, int deliveryDays, string message)
        {
            BestDistributor = bestDistributor;
            FinalTotalPrice = finalTotalPrice;
            DeliveryDays = deliveryDays;
            Message = message;
        }

        public BestQuoteResponse() { }
    }
}