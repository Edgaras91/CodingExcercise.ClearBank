### Test Description
In the 'PaymentService.cs' file you will find a method for making a payment. At a high level the steps for making a payment are:

 - Lookup the account the payment is being made from
 - Check the account is in a valid state to make the payment
 - Deduct the payment amount from the account's balance and update the account in the database

What we’d like you to do is refactor the code with the following things in mind:  
 - Adherence to SOLID principals
 - Testability  
 - Readability 

We’d also like you to add some unit tests to the ClearBank.DeveloperTest.Tests project to show how you would test the code that you’ve produced. The only specific ‘rules’ are:  

 - The solution should build.
 - The tests should all pass.
 - You should not change the method signature of the MakePayment method.

You are free to use any frameworks/NuGet packages that you see fit.  

You should plan to spend around 1 to 3 hours to complete the exercise.

### What was done

- I focused on refactoring just the `PaymentService.cs`
  - Moved `DataStore` fetching logic in it's own class `DataStoreProvider` (new)
  - Moved payment check logic into an `AccountService` (new)
  - Refactored the method itself
  - Added covering tests to cover every bit of logic it had originally

### What could be improved

- The app settings could use `IConfiguration` to better abstract in tests, instead of using real `ConfigurationManager.AppSettings["DataStoreType"]`
- `AccountService` takes in a Request's model Enum `PaymentScheme`. In real app this may be not accessible, so service may want to use service layer Enum instead. But for now it's YAGNI for current code base.
