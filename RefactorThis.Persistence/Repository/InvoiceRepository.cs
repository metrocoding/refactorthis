using System;
using RefactorThis.Persistence.Models;

namespace RefactorThis.Persistence.Repository
{
    public class InvoiceRepository : IRepository<Invoice>
    {
        private Invoice _invoice;

        public Invoice Get(string reference)
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