using System.ComponentModel.DataAnnotations;

namespace EitechPfe.DTOs.Requests
{
    public class LicenseCheckRequest
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive integer.")]
        public int UserId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "LicenseId must be a positive integer.")]
        public int LicenseId { get; set; }
    }
}
