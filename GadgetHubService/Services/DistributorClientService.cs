using GadgetHubService.Models;
using System.Net.Http.Json;

namespace GadgetHubService.Services
{
    public class DistributorClientService
    {
        private readonly HttpClient _http;

        public DistributorClientService(HttpClient http)
        {
            _http = http;
        }

        // 1. Existing GetQuoteAsync method (keep this)
        public async Task<DistributorQuote?> GetQuoteAsync(string baseUrl, string productId, int quantity)
        {
            try
            {
                var requestBody = new { ProductId = productId, Quantity = quantity };
                var response = await _http.PostAsJsonAsync($"{baseUrl}/api/Quotation", requestBody);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<DistributorQuote>();
                }
            }
            catch { /* Log error */ }
            return null;
        }

        public async Task<bool> PlaceOrderAsync(string url, OrderRequest order)
        {
            try
            {
                // Use 'url' directly without adding extra strings
                var response = await _http.PostAsJsonAsync(url, order);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}