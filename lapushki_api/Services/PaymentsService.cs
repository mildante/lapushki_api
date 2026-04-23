using lapushki_api.Data;
using lapushki_api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Yandex.Checkout.V3;
using static lapushki_api.Models.Payments;

namespace lapushki_api.Services
{
    public class PaymentsService : IPaymentsService
    {
        private readonly YooKassaOptions _options;
        private readonly ContextDb _contextDb;

        public PaymentsService(IOptions<YooKassaOptions> options, ContextDb context)
        {
            _options = options.Value;
            _contextDb = context;
        }
        public async Task<IActionResult> CreatePayment(CreatePaymentRequest request)
        {
            try
            {
                var client = new Client(_options.ShopId, _options.SecretKey);
                var payment = client.CreatePayment(new NewPayment
                {
                    Amount = new Amount
                    {
                        Value = request.Amount,
                        Currency = "RUB"
                    },
                    Confirmation = new Confirmation
                    {
                        Type = ConfirmationType.Redirect,
                        ReturnUrl = $"{_options.ReturnUrl}?appointmentId={request.AppointmentId}"
                    },
                    Capture = true,
                    Description = request.Description,
                });

                var appointment = await _contextDb.Appointments.FirstOrDefaultAsync(x => x.id == request.AppointmentId);

                if (appointment != null)
                {
                    appointment.payment_id = payment.Id;
                    appointment.status = "PendingPayment";
                    await _contextDb.SaveChangesAsync();
                }
                return new OkObjectResult( new
                {
                    status = true,
                    confirmationUrl = payment.Confirmation?.ConfirmationUrl,
                    paymentId = payment.Id
                });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new {status = false, error = ex.Message});
            }
        }

        public async Task<IActionResult> CheckPaymentStatusByAppointment(int appointmentId)
        {
            try
            {
                var appointment = await _contextDb.Appointments.FirstOrDefaultAsync(x => x.id == appointmentId);

                if (appointment == null || string.IsNullOrEmpty(appointment.payment_id))
                {
                    return new BadRequestObjectResult(new
                    {
                        status = false,
                        error = "Платеж для записи не найден"
                    });
                }

                var client = new Client(_options.ShopId, _options.SecretKey);
                var payment = client.GetPayment(appointment.payment_id);

                if (payment.Status == PaymentStatus.Succeeded)
                {
                    appointment.status = "Paid";
                    await _contextDb.SaveChangesAsync();
                }

                return new OkObjectResult(new
                {
                    status = true,
                    paymentStatus = payment.Status.ToString()
                });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    error = ex.Message
                });
            }
        }
    }
}
