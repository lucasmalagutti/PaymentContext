using PaymentContext.Domain.Entities;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var subscription = new Subscription(null);
            var student = new Student("Lucas", "Carvalho", "123456789", "lucas@email.com");
            student.AddSubscription(subscription);
        }
    }
}