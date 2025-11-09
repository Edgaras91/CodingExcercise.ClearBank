using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using NSubstitute;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services
{
    public class PaymentServiceTests
    {
        private PaymentService _sut;
        private IAccountDataStore _accountDataStore;
        private IAccountService _accountService;

        [SetUp]
        public void Setup()
        {
            _accountDataStore = Substitute.For<IAccountDataStore>();
            _accountService = Substitute.For<IAccountService>();

            var dataStoreProvider = Substitute.For<DataStoreProvider>();
            dataStoreProvider.GetAccountDataStore().Returns(_accountDataStore);

            _sut = new PaymentService(dataStoreProvider, _accountService);
        }

        [Test]
        public void MakePayment_ValidPayment_DeductsSuccessfully()
        {
            var account = new Account
            {
                AccountNumber = "87654321",
                Balance = 500.00m,
            };
            _accountDataStore.GetAccount("87654321").Returns(account);
            _accountService.IsPaymentAllowed(Arg.Any<Account>(), Arg.Any<PaymentScheme>(), Arg.Any<decimal>()).Returns(true);

            var request = new MakePaymentRequest
            {
                DebtorAccountNumber = "87654321",
                Amount = 100.00m,
            };

            var result = _sut.MakePayment(request);

            Assert.That(result.Success, Is.True);
            _accountDataStore.Received(1).UpdateAccount(Arg.Is<Account>(a => a.Balance == 400.00m));
        }

        [Test]
        public void MakePayment_InvalidPayment_DeductionFailed()
        {
            var account = new Account
            {
                AccountNumber = "87654321",
                Balance = 500.00m,
            };

            _accountDataStore.GetAccount("87654321").Returns(account);
            _accountService.IsPaymentAllowed(Arg.Any<Account>(), Arg.Any<PaymentScheme>(), Arg.Any<decimal>()).Returns(false);

            var request = new MakePaymentRequest
            {
                DebtorAccountNumber = "87654321",
                Amount = 100.00m,
            };

            var result = _sut.MakePayment(request);

            Assert.That(result.Success, Is.False);
            _accountDataStore.Received(0).UpdateAccount(Arg.Any<Account>());
        }
    }
}