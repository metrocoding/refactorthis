using System.Collections.Generic;
using System.Linq;

namespace RefactorThis.Persistence
{
    public class Invoice
    {
        public decimal Amount { get; set; }
        public decimal AmountPaid { get; set; }
        public List<Payment> Payments { get; set; }

        private bool HasPayed() => Payments.Sum(p => p.Amount) > 0;
        public bool IsFullyPaid() => HasPayed() && Amount == Payments.Sum(x => x.Amount);
        public bool IsOverPaid(decimal payedAmount) => HasPayed() && payedAmount > Remains();
        public bool HasNoPayment() => Payments == null || !Payments.Any();
        public decimal Remains() => Amount - AmountPaid;
    }
}