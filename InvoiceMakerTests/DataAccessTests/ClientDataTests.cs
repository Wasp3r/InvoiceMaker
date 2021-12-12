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
            var product = DataObjectsMock.MockProducts(2, DataAccess.ProductsManager);
            var clientToBeRemoved = DataObjectsMock.MockClient(1);
            var client = DataObjectsMock.MockClient(2);
            
            DataAccess.ClientsManager.Add(DataObjectsMock.MockClient(0));
            DataAccess.ClientsManager.Add(clientToBeRemoved);
            DataAccess.ClientsManager.Add(client);
            DataAccess.ClientsManager.Add(DataObjectsMock.MockClient(3));
            
            var invoice_0 = DataObjectsMock.MockInvoice(clientToBeRemoved, 0);
            invoice_0.Products.Add(DataObjectsMock.MockInvoiceEntry(product[0], 0));
            invoice_0.Products.Add(DataObjectsMock.MockInvoiceEntry(product[1], 0));
            DataAccess.InvoiceManager.Add(invoice_0);
            var invoice_1 = DataObjectsMock.MockInvoice(client, 1);
            invoice_1.Products.Add(DataObjectsMock.MockInvoiceEntry(product[0], 1));
            invoice_1.Products.Add(DataObjectsMock.MockInvoiceEntry(product[1], 1));
            DataAccess.InvoiceManager.Add(invoice_1);

            Assert.AreEqual(2, DataAccess.InvoiceManager.GetAll().Count());
            Assert.AreEqual(4, DataAccess.ClientsManager.GetAll().Count());
            DataAccess.ClientsManager.Remove(2);
            Assert.AreEqual(3, DataAccess.ClientsManager.GetAll().Count());
            Assert.IsEmpty(DataAccess.ClientsManager.GetByName("Test_1"));
            Assert.AreEqual(1, DataAccess.InvoiceManager.GetAll().Count());
        }

        [TestCase(1)]
        [TestCase(5)]
        public void GetClientsInvoicesTest(int number)
        {
            var products = DataObjectsMock.MockProducts(3, DataAccess.ProductsManager);
            var client = DataObjectsMock.MockClient(0);
            DataAccess.ClientsManager.Add(client);
            
            for (var i = 0; i < number; i++)
            {
                var invoice = DataObjectsMock.MockInvoice(client, i);
                invoice.Products.Add(DataObjectsMock.MockInvoiceEntry(products[0], i));
                invoice.Products.Add(DataObjectsMock.MockInvoiceEntry(products[1], i));
                invoice.Products.Add(DataObjectsMock.MockInvoiceEntry(products[2], i));
                DataAccess.InvoiceManager.Add(invoice);
            }
            
            Assert.AreEqual(number, client.Invoices.Count);
        }
    }
}