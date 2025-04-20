using System.Net.Http;
using System.Text;
using System.Text.Json;
using EitechPfe.DTOs.Requests;
using EitechPfe.DTOs.Responses;

namespace EitechPfe.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PaymentService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<PaymentResponse?> InitiatePayment(PaymentRequest paymentRequest)
        {
            var konnectApiKey = _configuration["Konnect:ApiKey"];
            if (string.IsNullOrEmpty(konnectApiKey))
                throw new InvalidOperationException("Konnect API key is missing in configuration.");

            var paymentData = new
            {
                receiverWalletId = _configuration["Konnect:ReceiverWalletId"],
                token = "TND",
                amount = paymentRequest.Amount,
                type = "immediate",
                description = paymentRequest.Description,
                acceptedPaymentMethods = paymentRequest.AcceptedPaymentMethods,
                lifespan = 10,
                checkoutForm = true,
                addPaymentFeesToAmount = true,
                firstName = paymentRequest.FirstName,
                lastName = paymentRequest.LastName,
                phoneNumber = paymentRequest.PhoneNumber,
                email = paymentRequest.Email,
                orderId = paymentRequest.ReferenceId.ToString(),
                theme = "dark"
            };

            var content = new StringContent(JsonSerializer.Serialize(paymentData), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-api-key", konnectApiKey);

            var konnectApiUrl = _configuration["Konnect:ApiUrl"];
            var response = await _httpClient.PostAsync(konnectApiUrl, content);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseDict = JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent);

            if (responseDict == null || !responseDict.ContainsKey("payUrl") || !responseDict.ContainsKey("paymentRef"))
                return null;

            return new PaymentResponse
            {
                PayUrl = responseDict["payUrl"],
                ReferenceId = responseDict["paymentRef"]
            };
        }
    }
}
