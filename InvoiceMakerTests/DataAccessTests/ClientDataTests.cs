using System.Linq;
using InvoiceMakerCore.Annotations.Builders;
using InvoiceMakerCore.Models;
using InvoiceMakerTests.MockHelpers;
using NUnit.Framework;

namespace InvoiceMakerTests.DataAccessTests
{
    public class ClientDataTests : DataAccessMockSetup
    {
        [TestCase(1)]
        [TestCase(5)]
        public void CreateClientsTest(int number)
        {
            for (var i = 0; i < number; i++)
            {
                DataAccess.ClientsManager.Add(DataObjectsMock.MockClient(i));
                Assert.AreEqual(1, DataAccess.ClientsManager.GetByName($"Test_{i}").Count());
            }
            
            Assert.AreEqual(number, DataAccess.ClientsManager.GetAll().Count());
        }

        [TestCase(1)]
        [TestCase(5)]
        public void GetClientByIdTest(int number)
        {
            for (var i = 0; i < number; i++)
            {
                DataAccess.ClientsManager.Add(DataObjectsMock.MockClient(i));
            }

            for (var i = 1; i < number +1; i++)
            {
                var client = DataAccess.ClientsManager.GetById(i);
                Assert.NotNull(client);
                Assert.AreEqual($"Test_{i - 1}", client.Name);
            }
        }

        [Test]
        public void UpdateClientTest()
        {
            DataAccess.ClientsManager.Add(DataObjectsMock.MockClient(0));
            var newData = new ClientModel() { Name = "UpdatedClient" };
            DataAccess.ClientsManager.Update(1, newData);
            
            Assert.AreEqual(newData.Name, DataAccess.ClientsManager.GetById(1).Name);
        }

        [Test]
        public void RemoveClientTest()
        {
            DataAccess.ClientsManager.Add(DataObjectsMock.MockClient(0));
            DataAccess.ClientsManager.Add(DataObjectsMock.MockClient(1));
            DataAccess.ClientsManager.Add(DataObjectsMock.MockClient(2));
            DataAccess.ClientsManager.Add(DataObjectsMock.MockClient(3));

            Assert.AreEqual(4, DataAccess.ClientsManager.GetAll().Count());
            DataAccess.ClientsManager.Remove(2);
            Assert.AreEqual(3, DataAccess.ClientsManager.GetAll().Count());
            Assert.IsEmpty(DataAccess.ClientsManager.GetByName("Test_1"));
        }
    }
}