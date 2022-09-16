using RefactorThis.Persistence;

namespace RefactorThis.Domain.Validator
{
    public class PaymentValidator : IPaymentValidator
    {
        public (Invoice, string) Validate(Invoice invoice, Payment payment)
        {
            var responseMessage = string.Empty;

            if (payment.Amount > invoice.Amount)
                return (invoice, "the payment is greater than the invoice amount");

            responseMessage = invoice.Amount == payment.Amount
                ? "invoice is now fully paid"
                : "invoice is now partially paid";
            invoice.AmountPaid = payment.Amount;
            invoice.Payments.Add(payment);

            return (invoice, responseMessage);
        }
    }
}