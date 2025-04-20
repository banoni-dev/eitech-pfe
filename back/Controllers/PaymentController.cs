using EitechPfe.DTOs.Requests;
using EitechPfe.DTOs.Responses;

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
    }
}
