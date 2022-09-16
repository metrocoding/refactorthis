using System;
using System.Collections.Generic;
using RefactorThis.Domain.Validators.Interfaces;
using RefactorThis.Domain.Validators.InvoicePaymentValidator;
using RefactorThis.Persistence;
using RefactorThis.Persistence.Models;
using RefactorThis.Persistence.Repository;

namespace RefactorThis.Domain
{
    public class InvoicePaymentProcessor
    {
        private readonly IRepository<Invoice> _invoiceRepository;
        private readonly List<IPaymentValidator> _paymentValidators = new List<IPaymentValidator>();

        public InvoicePaymentProcessor(IRepository<Invoice> invoiceRepository)
        {
            _paymentValidators.Add(new FreeInvoiceValidator());
            _paymentValidators.Add(new ExistingPaymentValidator());
            _paymentValidators.Add(new PaymentValidator());
            _invoiceRepository = invoiceRepository;
        }

        public string ProcessPayment(Payment payment)
        {
            var invoice = _invoiceRepository.Get(payment.Reference);
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