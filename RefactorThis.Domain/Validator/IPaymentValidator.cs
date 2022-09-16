using RefactorThis.Persistence;

namespace RefactorThis.Domain.Validator
{
    public interface IPaymentValidator
    {
        (Invoice, string) Validate(Invoice invoice, Payment payment);
    }
}