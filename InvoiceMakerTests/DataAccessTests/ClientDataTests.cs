using System.Linq;
using InvoiceMakerCore.Annotations.Builders;
using InvoiceMakerCore.Models;
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
                DataAccess.ClientsManager.AddClient(MockClient(i));
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
                DataAccess.ClientsManager.AddClient(MockClient(i));
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
            DataAccess.ClientsManager.AddClient(MockClient(0));
            var newData = new ClientModel() { Name = "UpdatedClient" };
            DataAccess.ClientsManager.UpdateClient(1, newData);
            
            Assert.AreEqual(newData.Name, DataAccess.ClientsManager.GetClientById(1).Name);
        }

        [Test]
        public void RemoveClientTest()
        {
            DataAccess.ClientsManager.AddClient(MockClient(0));
            DataAccess.ClientsManager.AddClient(MockClient(1));
            DataAccess.ClientsManager.AddClient(MockClient(2));
            DataAccess.ClientsManager.AddClient(MockClient(3));

            Assert.AreEqual(4, DataAccess.ClientsManager.GetAllClients().Count());
            DataAccess.ClientsManager.RemoveClient(2);
            Assert.AreEqual(3, DataAccess.ClientsManager.GetAllClients().Count());
            Assert.IsEmpty(DataAccess.ClientsManager.GetClientsByName("Test_1"));
        }

        private ClientModel MockClient(int number)
        {
            return new ClientBuilder()
                .AddName($"Test_{number}")
                .Build();
        }
    }
}