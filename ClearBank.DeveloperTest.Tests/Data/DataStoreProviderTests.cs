using System.Configuration;
using ClearBank.DeveloperTest.Data;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Data
{
    internal class DataStoreProviderTests
    {
        private DataStoreProvider _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new DataStoreProvider();
        }

        [Test]
        public void GetAccountDataStore_WithBackupConfig_ReturnsBackupStore()
        {
            ConfigurationManager.AppSettings["DataStoreType"] = "Backup";

            var dataStore = _sut.GetAccountDataStore();

            Assert.That(dataStore, Is.TypeOf<BackupAccountDataStore>());
        }

        [Test]
        public void GetAccountDataStore_WithNoConfig_ReturnsAccountStore()
        {
            ConfigurationManager.AppSettings["DataStoreType"] = null;

            var dataStore = _sut.GetAccountDataStore();

            Assert.That(dataStore, Is.TypeOf<AccountDataStore>());
        }
    }
}