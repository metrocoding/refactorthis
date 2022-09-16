using System;
using RefactorThis.Persistence;

namespace RefactorThis.Domain.Validator
{
    public class FreeInvoiceValidator : IPaymentValidator
    {
        public (Invoice, string) Validate(Invoice invoice, Payment payment)
        {
            var responseMessage = string.Empty;
            if (invoice.Amount != 0) return (invoice, responseMessage);
            if (invoice.HasNoPayment())
                responseMessage = "no payment needed";
            else
            {
                throw new InvalidOperationException(
                    "The invoice is in an invalid state, it has an amount of 0 and it has payments.");
            }

            return (invoice, responseMessage);
        }
    }
}