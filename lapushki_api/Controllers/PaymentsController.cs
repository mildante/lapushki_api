using lapushki_api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static lapushki_api.Models.Payments;

namespace lapushki_api.Controllers
{
    public class PaymentsService : Controller
    {
        private readonly IPaymentsService _paymentsService;
        public PaymentsService(IPaymentsService paymentsService)
        {
            _paymentsService = paymentsService;
        }

        [HttpPost]
        [Route("createPayment")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest request)
        {
            return await _paymentsService.CreatePayment(request);
        }

        [HttpGet]
        [Route("checkPaymentStatus")]
        public async Task<IActionResult> CheckPaymentStatus(string paymentId)
        {
            return await _paymentsService.CheckPaymentStatus(paymentId);
        }
    }
}
