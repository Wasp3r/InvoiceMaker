using System.Linq;
using InvoiceMakerCore.Models;
using NUnit.Framework;

namespace InvoiceMakerTests.DataTests
{
    public class ClientDataTests : DataAccessMockSetup
    {
        [TestCase(1)]
        [TestCase(5)]
        public void CreateClientsTest(int number)
        {
            for (var i = 0; i < number; i++)
            {
                DataAccess.ClientsManager.CreateClient($"Test_{i}");
                Assert.AreEqual(1, DataAccess.ClientsManager.GetClientsByName($"Test_{i}").Count());
            }
            
            Assert.AreEqual(number, DataAccess.ClientsManager.GetAllClients().Count());
        }

        [TestCase(1)]
        [TestCase(5)]
        public void GetClientByIdTest(int number)
        {
            for (var i = 0; i < number; i++)
            {
                DataAccess.ClientsManager.CreateClient($"Test_{i}");
            }

            for (var i = 1; i < number +1; i++)
            {
                var client = DataAccess.ClientsManager.GetClientById(i);
                Assert.NotNull(client);
                Assert.AreEqual($"Test_{i - 1}", client.Name);
            }
        }

        [Test]
        public void UpdateClientTest()
        {
            DataAccess.ClientsManager.CreateClient("TestClient");
            var newData = new ClientModel() { Name = "UpdatedClient" };
            DataAccess.ClientsManager.UpdateClient(1, newData);
            
            Assert.AreEqual(newData.Name, DataAccess.ClientsManager.GetClientById(1).Name);
        }

        [Test]
        public void RemoveClientTest()
        {
            DataAccess.ClientsManager.CreateClient("Test_0");
            DataAccess.ClientsManager.CreateClient("Test_1");
            DataAccess.ClientsManager.CreateClient("Test_2");
            DataAccess.ClientsManager.CreateClient("Test_3");
            
            Assert.AreEqual(4, DataAccess.ClientsManager.GetAllClients().Count());
            DataAccess.ClientsManager.RemoveClient(2);
            Assert.AreEqual(3, DataAccess.ClientsManager.GetAllClients().Count());
            Assert.IsEmpty(DataAccess.ClientsManager.GetClientsByName("Test_1"));
        }
    }
}