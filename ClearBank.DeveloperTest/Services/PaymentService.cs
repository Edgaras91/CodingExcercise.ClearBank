using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService(DataStoreProvider dataStoreProvider, IAccountService accountService) : IPaymentService
    {
        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var accountDataStore = dataStoreProvider.GetAccountDataStore();

            var account = accountDataStore.GetAccount(request.DebtorAccountNumber);

            var paymentAllowed = accountService.IsPaymentAllowed(account, request.PaymentScheme, request.Amount);

            if (paymentAllowed == false)
            {
                return new MakePaymentResult
                {
                    Success = false
                };
            }

            account.Balance -= request.Amount;
            accountDataStore.UpdateAccount(account);

            return new MakePaymentResult
            {
                Success = true
            };
        }
    }
}