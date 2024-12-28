using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands
{
    public class CreatePayPalSubscriptionCommand : Notifiable<Notification>, ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public string TransactionCode { get; set; }
        public string PaymentNumber { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }
        public string PayerDocument { get; set; }
        public EDocumentType PayerDocumentType { get; set; }
        public string Payer { get; set; }
        public string PayerEmail { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
            .Requires()
            .IsNotNullOrEmpty(Email, "Email.Address", "Email não pode ser vazio.")
            .IsEmail(Email, "Email.Address", "Email inválido")
            .IsNotNullOrEmpty(FirstName, "Name.FirstName", "Nome não pode ser vazio.")
            .IsNotNullOrEmpty(LastName, "Name.LastName", "Sobrenome não pode ser vazio.")
            .IsTrue(ZipCode.Length == 8, "Address.ZipCode", "CEP deve ter 8 caracteres")
            .IsNullOrEmpty(Street, "Address.Street", "Rua não pode zer vazio.")
            .IsNullOrEmpty(Number, "Address.Number", "Número não pode zer vazio.")
            .IsNullOrEmpty(Neighborhood, "Address.Neighborhood", "Bairro não pode zer vazio.")
            .IsNullOrEmpty(City, "Address.City", "Cidade não pode zer vazio.")
            .IsNullOrEmpty(Country, "Address.Country", "País não pode zer vazio.")
            .IsNullOrEmpty(ZipCode, "Address.ZipCode", "CEP não pode zer vazio.")
            );
        }
    }
}