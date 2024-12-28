using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable<Notification>,
    IHandler<CreateBoletoSubscriptionCommand>,
    IHandler<CreatePayPalSubscriptionCommand>,
    IHandler<CreateCreditCardSubscriptionCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository studentRepository, IEmailService emailService)
        {
            _studentRepository = studentRepository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar assinatura");
            }
            //Verifica se o documento já está cadastrado
            if (_studentRepository.DocumentExists(command.Document))
                AddNotification("Document", "Este documento já está em uso");

            //Verifica se Email já está cadastrado
            if (_studentRepository.EmailExists(command.Email))
                AddNotification("Email", "Este email já está em uso");

            //Gerar VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            //Gerar Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.BarCode,
                command.BoletoNumber,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                address,
                new Document(command.PayerDocument, command.PayerDocumentType),
                command.Payer,
                email
            );
            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Checar as validações
            if (!IsValid)
                return new CommandResult(false, "Não foi possível realizar assinatura");

            //Salvar as Informações
            _studentRepository.CreateSubscription(student);

            //Envial email de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo", "Sua assinatura foi realizada com sucesso");

            //Retornar informações 
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar assinatura");
            }
            //Verifica se o documento já está cadastrado
            if (_studentRepository.DocumentExists(command.Document))
                AddNotification("Document", "Este documento já está em uso");

            //Verifica se Email já está cadastrado
            if (_studentRepository.EmailExists(command.Email))
                AddNotification("Email", "Este email já está em uso");

            //Gerar VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            //Gerar Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(
                command.TransactionCode,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                address,
                new Document(command.PayerDocument, command.PayerDocumentType),
                command.Payer,
                email
            );
            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Checar as validações
            if (!IsValid)
                return new CommandResult(false, "Não foi possível realizar assinatura");

            //Salvar as Informações
            _studentRepository.CreateSubscription(student);

            //Envial email de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo", "Sua assinatura foi realizada com sucesso");

            //Retornar informações 
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar assinatura");
            }
            //Verifica se o documento já está cadastrado
            if (_studentRepository.DocumentExists(command.Document))
                AddNotification("Document", "Este documento já está em uso");

            //Verifica se Email já está cadastrado
            if (_studentRepository.EmailExists(command.Email))
                AddNotification("Email", "Este email já está em uso");

            //Gerar VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            //Gerar Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new CreditCardPayment(
                command.CardHolderName,
                command.CardNumber,
                command.LastTransactionNumber,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                address,
                new Document(command.PayerDocument, command.PayerDocumentType),
                command.Payer,
                email
            );
            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Checar as validações
            if (!IsValid)
                return new CommandResult(false, "Não foi possível realizar assinatura");

            //Salvar as Informações
            _studentRepository.CreateSubscription(student);

            //Envial email de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo", "Sua assinatura foi realizada com sucesso");

            //Retornar informações 
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }

}
