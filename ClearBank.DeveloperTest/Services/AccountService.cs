using System;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class AccountService : IAccountService
    {
        public bool IsPaymentAllowed(Account account, PaymentScheme paymentScheme, decimal amount)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            switch (paymentScheme)
            {
                case PaymentScheme.Bacs:
                    if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
                    {
                        return false;
                    }

                    break;
                case PaymentScheme.FasterPayments:
                    if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
                    {
                        return false;
                    }

                    if (account.Balance < amount)
                    {
                        return false;
                    }

                    break;
                case PaymentScheme.Chaps:
                    if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
                    {
                        return false;
                    }

                    if (account.Status != AccountStatus.Live)
                    {
                        return false;
                    }

                    break;
            }

            return true;
        }
    }
}