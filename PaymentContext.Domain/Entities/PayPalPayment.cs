using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;

public class PayPalPayment : Payment
{
    public PayPalPayment(
    string transactionCode,
    DateTime paidDate,
    DateTime expireDate,
    decimal total,
    decimal totalPaid,
    Address address,
    Document document,
    string payer,
    Email email) : base(
    paidDate,
    expireDate,
    total,
    totalPaid,
    address,
    document,
    payer,
    email)
    {
        TransactionCode = transactionCode;
    }

    public string TransactionCode { get; set; }
}