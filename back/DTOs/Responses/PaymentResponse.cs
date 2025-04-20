namespace EitechPfe.DTOs.Responses
{
    public class PaymentResponse
    {
        public string? PayUrl { get; set; } // URL for the payment page
        public string? ReferenceId { get; set; } // Unique reference ID for the payment
    }
}
