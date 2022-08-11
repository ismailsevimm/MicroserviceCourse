using MSCourse.Web.Models.PaymentModels;
using MSCourse.Web.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MSCourse.Web.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient
            )
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ReceivePayment(PayWithCardInput payWithCardInput)
        {
            var response = await _httpClient.PostAsJsonAsync<PayWithCardInput>("payments", payWithCardInput);

            return response.IsSuccessStatusCode;
        }
    }
}
