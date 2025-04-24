using EitechPfe.DTOs.Requests;
using EitechPfe.DTOs.Responses;
using EitechPfe.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EitechPfe.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("initiate")]
        public async Task<IActionResult> InitiatePayment([FromBody] PaymentRequest paymentRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid payment request.");

            var paymentResponse = await _paymentService.InitiatePayment(paymentRequest);
            if (paymentResponse == null)
                return BadRequest("Failed to initiate payment.");

            return Ok(paymentResponse);
        }

        // [HttpGet("webhook")]
        // public async Task<IActionResult> HandleWebhook([FromQuery] string payment_ref)
        // {
        //     if (string.IsNullOrEmpty(payment_ref))
        //         return BadRequest("Missing payment reference.");

        //     var paymentDetails = await _paymentService.GetPaymentDetails(payment_ref);
        //     if (paymentDetails == null)
        //         return NotFound("Payment details not found.");

        //     // Process the payment details (e.g., update order status)
        //     // Example: if (paymentDetails.Status == "SUCCESS") { ... }

        //     return Ok(paymentDetails);
        // }

        [HttpGet("{paymentRef}")]
        public async Task<IActionResult> GetPaymentDetails(string paymentRef)
        {
            Console.WriteLine($"Payment Referenceeeeeee: {paymentRef}");
            var paymentDetails = await _paymentService.GetPaymentDetails(paymentRef);
            if (paymentDetails == null)
                return NotFound("Payment details not found.");

            return Ok(paymentDetails);
        }
    }
}
