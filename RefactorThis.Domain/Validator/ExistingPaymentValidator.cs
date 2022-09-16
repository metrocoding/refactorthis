using System;
using System.Linq;
using RefactorThis.Persistence;

namespace RefactorThis.Domain.Validator
{
    public class ExistingPaymentValidator : IPaymentValidator
    {
        public (Invoice, string) Validate(Invoice invoice, Payment payment)
        {
            var responseMessage = "";
            if (invoice.Payments == null || !invoice.Payments.Any()) return (invoice, responseMessage);

            // we could put this is null validator also
            if (invoice == null)
                throw new InvalidOperationException("There is no invoice matching this payment");

            if (invoice.IsFullyPaid()) responseMessage = "invoice was already fully paid";
            else if (invoice.IsOverPaid(payment.Amount))
                responseMessage = "the payment is greater than the partial amount remaining";
            else
            {
                responseMessage = invoice.Remains() == payment.Amount
                    ? "final partial payment received, invoice is now fully paid"
                    : "another partial payment received, still not fully paid";

                invoice.AmountPaid += payment.Amount;
                invoice.Payments.Add(payment);
            }

            return (invoice, responseMessage);
        }
    }
}