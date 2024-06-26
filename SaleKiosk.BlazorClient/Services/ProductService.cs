using Newtonsoft.Json;
using CarRental.SharedKernel.Dto;

namespace CarRental.BlazorClient.Services
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDto>> GetAll();
    }

    public class ServiceService : IServiceService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _carRentalServerUrl;

        public ServiceService(HttpClient httpClient, IConfiguration configuration)
        {
            this._httpClient = httpClient;
            this._configuration = configuration;
            this._carRentalServerUrl = _configuration.GetSection("CarRentalServerUrl").Value;
        }

        public async Task<IEnumerable<ServiceDto>> GetAll()
        {
            var response = await _httpClient.GetAsync("/product");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<IEnumerable<ServiceDto>>(content);
                //foreach (var p in products)
                //{
                //    p.ImageUrl = _saleKioskServerUrl + p.ImageUrl;
                //}

                return products;
            }

            return new List<ServiceDto>();
        }
    }
}
