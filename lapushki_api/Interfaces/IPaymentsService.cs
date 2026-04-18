using Microsoft.AspNetCore.Mvc;
using static lapushki_api.Models.Payments;

namespace lapushki_api.Interfaces
{
    public interface IPaymentsService
    {
        Task<IActionResult> CreatePayment(CreatePaymentRequest request);
        Task<IActionResult> CheckPaymentStatus(string paymentId);
    }
}
