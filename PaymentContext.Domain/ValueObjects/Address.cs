using Flunt.Notifications;
using Flunt.Validations;
using Flunt.Br;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public Address(string street, string number, string neighborhood, string city, string state, string country, string zipCode)
        {
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;

            AddNotifications(new Contract<Notification>()
            .Requires()
            .IsTrue(ZipCode.Length == 8, "Address.ZipCode", "CEP deve ter 8 caracteres")
            .IsNullOrEmpty(Street, "Address.Street", "Rua não pode zer vazio.")
            .IsNullOrEmpty(Number, "Address.Number", "Número não pode zer vazio.")
            .IsNullOrEmpty(Neighborhood, "Address.Neighborhood", "Bairro não pode zer vazio.")
            .IsNullOrEmpty(City, "Address.City", "Cidade não pode zer vazio.")
            .IsNullOrEmpty(Country, "Address.Country", "País não pode zer vazio.")
            .IsNullOrEmpty(ZipCode, "Address.ZipCode", "CEP não pode zer vazio.")
            );
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
    }
}