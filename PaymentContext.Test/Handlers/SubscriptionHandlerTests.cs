using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new Domain.Commands.CreateBoletoSubscriptionCommand();
            command.FirstName = "Ana";
            command.LastName = "Maria";
            command.Document = "99999999999";
            command.Email = "ana@email.com";
            command.BarCode = "44580101";
            command.BoletoNumber = "227593241";
            command.PaymentNumber = "24252378";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 133;
            command.TotalPaid = 133;
            command.Payer = "Ana Maria";
            command.PayerDocument = "053521012";
            command.PayerDocumentType = EDocumentType.CPF;
            command.PayerEmail = "ana@email.com";
            command.Street = "Rua 1";
            command.Number = " 34";
            command.Neighborhood = "Bairro 2";
            command.City = "São Paulo";
            command.State = "São Paulo";
            command.Country = "Brasil";
            command.ZipCode = "08333331";

            handler.Handle(command);
            Assert.AreEqual(false, handler.IsValid);
        }
    }
}