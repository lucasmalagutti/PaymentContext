using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public Email(string address)
        {
            Address = address;

            AddNotifications(new Contract<Notification>()
            .Requires()
            .IsNotNullOrEmpty(Address, "Email.Address", "Email não pode ser vazio.")
            .IsEmail(Address, "Email.Address", "Email inválido")
            );
        }

        public string Address { get; private set; }
    }
}