using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new Contract<Notification>()
            .Requires()
            .IsNotNullOrEmpty(FirstName, "Name.FirstName", "Nome não pode ser vazio.")
            .IsNotNullOrEmpty(LastName, "Name.LastName", "Sobrenome não pode ser vazio.")
            );
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}