using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services;

public interface IAccountService
{
    bool IsPaymentAllowed(Account account, PaymentScheme paymentScheme, decimal amount);
}