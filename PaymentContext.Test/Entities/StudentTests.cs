using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{

    [TestClass]
    public class StudentTests
    {
        private readonly Student _student;
        private readonly Subscription _subscription;
        private readonly Document _document;
        private readonly Email _email;
        private readonly Name _name;
        private readonly Address _address;

        public StudentTests()
        {
            _name = new Name("Ana", "Carvalho");
            _document = new Document("123456789", EDocumentType.CPF);
            _email = new Email("ana@carvalho.com");
            _address = new Address("Rua", "123", "Bairro", "Cidade", "São Paulo", "Brasil", "08000000");
            _student = new Student(_name, _document, _email);
            _subscription = new Subscription(null);
        }
        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, _address, _document, "Ana Carvalho", _email);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsFalse(_student.IsValid);
        }
        [TestMethod]
        public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
        {
            _student.AddSubscription(_subscription);

            Assert.IsFalse(_student.IsValid);
        }
        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription()
        {
            var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, _address, _document, "Ana Carvalho", _email);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.IsValid);
        }
    }
}