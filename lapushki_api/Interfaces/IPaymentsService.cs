using Microsoft.AspNetCore.Mvc;
using static lapushki_api.Models.Payments;

namespace lapushki_api.Interfaces
{
    public interface IPaymentsService
    {
        Task<IActionResult> CreatePayment(CreatePaymentRequest request);
        Task<IActionResult> CheckPaymentStatusByAppointment(int appointmentId);
    }
}
