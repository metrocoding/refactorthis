using RefactorThis.Persistence.Models;

namespace RefactorThis.Domain.Validators.Interfaces
{
    public interface IPaymentValidator
    {
        (Invoice, string) Validate(Invoice invoice, Payment payment);
    }
}