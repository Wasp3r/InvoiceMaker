using System.Collections.Generic;
using System.Linq;
using InvoiceMakerCore.Annotations.Builders;
using InvoiceMakerCore.Models;
using InvoiceMakerTests.MockHelpers;
using NUnit.Framework;

namespace InvoiceMakerTests.DataAccessTests
{
    public class InvoiceDataTests : DataAccessMockSetup
    {
        [TestCase(1)]
        [TestCase(5)]
        public void CreateInvoicesTest(int number)
        {
            var mockedProducts = DataObjectsMock.MockProducts(5, DataAccess.ProductsManager);
            for (var i = 0; i < number; i++)
            {
                var client = DataObjectsMock.MockClient(i);
                DataAccess.ClientsManager.Add(client);
                
                var invoice = DataObjectsMock.MockInvoice(client, i);
                invoice.Products.Add(DataObjectsMock.MockInvoiceEntry(mockedProducts[0], 1));
                invoice.Products.Add(DataObjectsMock.MockInvoiceEntry(mockedProducts[1], 2));
                invoice.Products.Add(DataObjectsMock.MockInvoiceEntry(mockedProducts[2], 3));
                invoice.Products.Add(DataObjectsMock.MockInvoiceEntry(mockedProducts[3], 4));
                invoice.Products.Add(DataObjectsMock.MockInvoiceEntry(mockedProducts[4], 5));

                DataAccess.InvoiceManager.Add(invoice);

                var createdInvoice = DataAccess.InvoiceManager.GetById(i + 1);
                Assert.NotNull(createdInvoice);
                Assert.AreEqual(5, createdInvoice.Products.Count);
                Assert.AreEqual(5, DataAccess.ProductsManager.GetAll().Count());
            }
            
            Assert.IsNotEmpty(DataAccess.InvoiceManager.GetAll());
            Assert.AreEqual(number, DataAccess.InvoiceManager.GetAll().Count());
            Assert.AreEqual(number, DataAccess.ClientsManager.GetAll().Count());
            Assert.AreEqual(5, DataAccess.ProductsManager.GetAll().Count());
        }

        [Test]
        public void CreateMultipleInvoicesToOneClientTest()
        {
            var client = DataObjectsMock.MockClient(1);
            var products = DataObjectsMock.MockProducts(3, DataAccess.ProductsManager);

            for (var i = 0; i < 3; i++)
            {
                var invoice = DataObjectsMock.MockInvoice(client, i);
                invoice.Products.Add(DataObjectsMock.MockInvoiceEntry(products[0], i));
                invoice.Products.Add(DataObjectsMock.MockInvoiceEntry(products[1], i));
                invoice.Products.Add(DataObjectsMock.MockInvoiceEntry(products[2], i));

                DataAccess.InvoiceManager.Add(invoice);
            }
            
            Assert.AreEqual(3, DataAccess.InvoiceManager.GetAll().Count(x => x.Client.Id == 1));
        }

        [Test]
        public void UpdateInvoiceTest()
        {
            var products = DataObjectsMock.MockProducts(3, DataAccess.ProductsManager);
            var client_0 = DataObjectsMock.MockClient(0);
            var client_1 = DataObjectsMock.MockClient(1);
            var invoice = DataObjectsMock.MockInvoice(client_0, 0);
            foreach (var product in products)
            {
                invoice.Products.Add(DataObjectsMock.MockInvoiceEntry(product, 1));
            }
            
            DataAccess.InvoiceManager.Add(invoice);
            Assert.NotNull(DataAccess.InvoiceManager.GetById(1));
            Assert.AreEqual(3, DataAccess.InvoiceManager.GetInvoiceSum(invoice));

            invoice.Products[0].Quantity = 2;
            invoice.Client = client_1;
            DataAccess.SaveChanges();

            var dbInvoice = DataAccess.InvoiceManager.GetById(1);
            Assert.AreEqual(4, DataAccess.InvoiceManager.GetInvoiceSum(dbInvoice));
            Assert.AreEqual(client_1, dbInvoice.Client);
            Assert.IsEmpty(client_0.Invoices);
        }

        [TestCase(1)]
        [TestCase(5)]
        public void CheckInvoiceSumTest(int number)
        {
            var products = DataObjectsMock.MockProducts(number, DataAccess.ProductsManager);
            var client = DataObjectsMock.MockClient(0);

            var invoice = DataObjectsMock.MockInvoice(client, 0);
            foreach (var product in products)
            {
                invoice.Products.Add(DataObjectsMock.MockInvoiceEntry(product, 2));
            }
            DataAccess.InvoiceManager.Add(invoice);
            var dbInvoice = DataAccess.InvoiceManager.GetById(1);
            
            Assert.AreEqual(4 * number, DataAccess.InvoiceManager.GetInvoiceSum(dbInvoice));
        }
    }
}