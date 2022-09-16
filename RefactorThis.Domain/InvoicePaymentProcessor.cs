using System;
using System.Collections.Generic;
using RefactorThis.Domain.Validator;
using RefactorThis.Persistence;

namespace RefactorThis.Domain
{
    public class InvoicePaymentProcessor
    {
        private readonly InvoiceRepository _invoiceRepository;
        private readonly List<IPaymentValidator> _paymentValidators = new List<IPaymentValidator>();

        public InvoicePaymentProcessor(InvoiceRepository invoiceRepository)
        {
            _paymentValidators.Add(new FreeInvoiceValidator());
            _paymentValidators.Add(new ExistingPaymentValidator());
            _paymentValidators.Add(new PaymentValidator());
            _invoiceRepository = invoiceRepository;
        }

        public string ProcessPayment(Payment payment)
        {
            var invoice = _invoiceRepository.GetInvoice(payment.Reference);
            if (invoice == null) throw new InvalidOperationException("There is no invoice matching this payment");

            var responseMessage = string.Empty;
            foreach (var validator in _paymentValidators)
            {
                if (string.IsNullOrEmpty(responseMessage))
                    (invoice, responseMessage) = validator.Validate(invoice, payment);
            }

            _invoiceRepository.Save();
            return responseMessage;
        }
    }
}