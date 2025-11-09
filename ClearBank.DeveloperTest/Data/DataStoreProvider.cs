namespace ClearBank.DeveloperTest.Data
{
    public class DataStoreProvider
    {
        public virtual IAccountDataStore GetAccountDataStore()
        {
            var dataStoreType = System.Configuration.ConfigurationManager.AppSettings["DataStoreType"];
            if (dataStoreType == "Backup")
            {
                return new BackupAccountDataStore();
            }

            return new AccountDataStore();
        }
    }
}