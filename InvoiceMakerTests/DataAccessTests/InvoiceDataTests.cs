using System.Collections.Generic;
using System.Linq;
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
            var mockedProducts = CreateProducts(5);
            for (var i = 0; i < number; i++)
            {
                var client = DataObjectsMock.MockClient(i);
                DataAccess.ClientsManager.Add(client);
                
                var invoice = DataObjectsMock.MockInvoice(client, i);
                invoice.Products = new List<InvoiceProductEntryModel>()
                {
                    DataObjectsMock.MockInvoiceEntry(mockedProducts[0], 1),
                    DataObjectsMock.MockInvoiceEntry(mockedProducts[1], 2),
                    DataObjectsMock.MockInvoiceEntry(mockedProducts[2], 3),
                    DataObjectsMock.MockInvoiceEntry(mockedProducts[3], 4),
                    DataObjectsMock.MockInvoiceEntry(mockedProducts[4], 5),
                };

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
        
        private List<ProductModel> CreateProducts(int productsNumber)
        {
            var products = new List<ProductModel>();

            for (var i = 0; i < productsNumber; i++)
            {
                var product = DataObjectsMock.MockProduct(i);
                DataAccess.ProductsManager.Add(product);
                products.Add(product);
            }

            return products;
        }
    }
}