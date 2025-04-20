namespace EitechPfe.DTOs.Requests
{
    public class PaymentRequest
    {
        public string ActionType { get; set; } = string.Empty; // e.g., "subscription" or "license"
        public int ReferenceId { get; set; } // ID of the subscription tier or license
        public int UserId { get; set; } // ID of the user making the payment
        public decimal Amount { get; set; } // Payment amount
        public string Description { get; set; } = string.Empty; // Payment description
        public string FirstName { get; set; } = string.Empty; // User's first name
        public string LastName { get; set; } = string.Empty; // User's last name
        public string PhoneNumber { get; set; } = string.Empty; // User's phone number
        public string Email { get; set; } = string.Empty; // User's email
        public string[] AcceptedPaymentMethods { get; set; } = new[] { "wallet", "e-DINAR" }; // Accepted payment methods
    }
}
