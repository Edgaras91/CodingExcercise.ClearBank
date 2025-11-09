using System;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services
{
    public class AccountServiceTests
    {
        private AccountService _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new AccountService();
        }

        public static object[] PositiveAccountChargeCases =
        [
            new object[] {new Account {AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs}, PaymentScheme.Bacs, 100m},
            new object[] {new Account {AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments, Balance = 200}, PaymentScheme.FasterPayments, 100m},
            new object[] {new Account {AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps, Status = AccountStatus.Live}, PaymentScheme.Chaps, 100m}
        ];

        public static object[] NegativeAccountChargeCases =
        [
            new object[] {new Account {AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments}, PaymentScheme.Bacs, 100m},
            new object[] {new Account {AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs, Balance = 200}, PaymentScheme.FasterPayments, 100m},
            new object[] {new Account {AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments, Balance = 200m}, PaymentScheme.FasterPayments, 500m},
            new object[] {new Account {AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps, Status = AccountStatus.Disabled}, PaymentScheme.Chaps, 100m},
            new object[] {new Account {AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps, Status = AccountStatus.InboundPaymentsOnly}, PaymentScheme.Chaps, 100m},
            new object[] {new Account {AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments, Status = AccountStatus.Live}, PaymentScheme.Chaps, 100m}
        ];

        [TestCaseSource(nameof(PositiveAccountChargeCases))]
        public void IsPaymentAllowed_ValidPayment_ReturnsTrue(Account account, PaymentScheme paymentScheme, decimal chargeAmount)
        {
            var paymentAllowed = _sut.IsPaymentAllowed(account, paymentScheme, chargeAmount);
            Assert.That(paymentAllowed, Is.True);
        }

        [TestCaseSource(nameof(NegativeAccountChargeCases))]
        public void IsPaymentAllowed_InvalidPayment_ReturnsFalse(Account account, PaymentScheme paymentScheme, decimal chargeAmount)
        {
            var paymentAllowed = _sut.IsPaymentAllowed(account, paymentScheme, chargeAmount);
            Assert.That(paymentAllowed, Is.False);
        }

        [Test]
        public void IsPaymentAllowed_NullAccount_ShouldThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() => _sut.IsPaymentAllowed(null, PaymentScheme.Bacs, 100m));
        }
    }
}