using CarRental.BlazorClient.ViewModels;

namespace CarRental.BlazorClient.Services
{
    public interface IOrderService
    {
        Task SubmitOrder(List<CartItem> cart, string orderMessage, int CustomerId);
    }

    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task SubmitOrder(List<CartItem> cart, string orderMessage, int customerId)
        {
           
           
        }
    }
}
