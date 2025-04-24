namespace EitechPfe.DTOs.Responses
{
    public class PaymentDetailsResponse
    {
        public string? PaymentRef { get; set; } // Unique payment reference ID
        public string? Status { get; set; } // Payment status (e.g., SUCCESS, FAILED)
        public decimal Amount { get; set; } // Payment amount
        public string? Currency { get; set; } // Payment currency
        public string? Description { get; set; } // Payment description
        public DateTime? PaymentDate { get; set; } // Date of payment
    }
}
