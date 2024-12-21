using Flunt.Br;
using Flunt.Br.Extensions;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Document : ValueObject
    {
        public Document(string number, EDocumentType type)
        {
            Number = number;
            Type = type;

            AddNotifications(new Contract<Notification>()
            .Requires()
            .IsNotNullOrEmpty(Number, "Document.Number", "Número não pode ser vazio.")
            .IsTrue(Validate(), "Document.Number", "Documento inválido.")
            );

        }

        public string Number { get; private set; }
        public EDocumentType Type { get; private set; }
        private bool Validate()
        {
            var contract = new Contract();

            if (Type == EDocumentType.CPF)
                contract = new Contract().IsCpf(Number, "Document.Number", "CPF inválido.");

            if (Type == EDocumentType.CNPJ)
                contract = new Contract().IsCnpj(Number, "Document.Number", "CNPJ inválido.");

            AddNotifications(contract.Notifications);

            return IsValid;
        }
    }
}