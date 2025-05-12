using System.ComponentModel.DataAnnotations;

namespace EitechPfe.DTOs.Requests
{
    public class SubscriptionCheckRequest
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive integer.")]
        public int UserId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "ProductId must be a positive integer.")]
        public int ProductId { get; set; }
    }
}
