using System;

namespace RefactorThis.Persistence
{
    public class InvoiceRepository
    {
        private Invoice _invoice;

        public Invoice GetInvoice(string reference)
        {
            Console.WriteLine($"Getting invoice: {reference}");
            return _invoice;
        }

        public void Save()
        {
            Console.WriteLine("Saving invoice to database");
        }

        public void Add(Invoice invoice)
        {
            _invoice = invoice;
        }
    }
}