using EitechPfe.DTOs.Requests;
using EitechPfe.DTOs.Responses;

namespace EitechPfe.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponse?> InitiatePayment(PaymentRequest paymentRequest);
    }
}
