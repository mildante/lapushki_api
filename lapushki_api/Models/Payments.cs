namespace lapushki_api.Models
{
    public class Payments
    {
        public class YooKassaOptions
        {
            public string ShopId { get; set; } = "";
            public string SecretKey { get; set; } = "";
            public string ReturnUrl { get; set; } = "";
        }

        public class CreatePaymentRequest
        {
            public decimal Amount { get; set; }
            public string Description { get; set; } = "";
        }

        public class CreatePaymentResponse
        {
            public bool Status { get; set; }
            public string? ConfirmationUrl { get; set; }
            public string? PaymentId { get; set; }
            public string? Error { get; set; }
        }
    }
}
