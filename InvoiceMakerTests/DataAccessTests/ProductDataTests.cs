using System.Data;
using System.Linq;
using InvoiceMakerCore.Annotations.Builders;
using InvoiceMakerCore.Models;
using InvoiceMakerTests.MockHelpers;
using NUnit.Framework;

namespace InvoiceMakerTests.DataAccessTests
{
    public class ProductDataTests : DataAccessMockSetup
    {
        [TestCase(1)]
        [TestCase(5)]
        public void CreateProductsTest(int number)
        {
            for (var i = 0; i < number; i++)
            {
                var newProduct = DataObjectsMock.MockProduct(i);
                DataAccess.ProductsManager.Add(newProduct);
                Assert.NotNull(DataAccess.ProductsManager.GetByName(newProduct.Name));
            }
            
            Assert.AreEqual(number, DataAccess.ProductsManager.GetAll().Count());
        }

        [Test]
        public void UpdateProductTest()
        {
            var product = DataObjectsMock.MockProduct(0);
            DataAccess.ProductsManager.Add(product);
            
            Assert.AreEqual("Product_0", product.Name);
            product.Name = "Product Client";
            DataAccess.SaveChanges();
            
            Assert.AreEqual("Product Client", DataAccess.ProductsManager.GetById(1).Name);
        }

        [Test]
        public void RemoveProductTest()
        {
            DataAccess.ProductsManager.Add(DataObjectsMock.MockProduct(0));
            DataAccess.ProductsManager.Add(DataObjectsMock.MockProduct(1));
            DataAccess.ProductsManager.Add(DataObjectsMock.MockProduct(2));
            DataAccess.ProductsManager.Add(DataObjectsMock.MockProduct(3));
            
            Assert.AreEqual(4, DataAccess.ProductsManager.GetAll().Count());
            DataAccess.ProductsManager.Remove(2);
            Assert.AreEqual(3, DataAccess.ProductsManager.GetAll().Count());
            Assert.IsEmpty(DataAccess.ClientsManager.GetByName("Product_1"));
        }

        [Test]
        public void RemoveUsedProductTest()
        {
            var client = DataObjectsMock.MockClient(0);
            var invoice = DataObjectsMock.MockInvoice(client, 0);
            var product = DataObjectsMock.MockProduct(0);
            invoice.Products.Add(DataObjectsMock.MockInvoiceEntry(product, 0));

            DataAccess.ProductsManager.Add(product);
            DataAccess.ClientsManager.Add(client);
            DataAccess.InvoiceManager.Add(invoice);
            
            Assert.Throws<ReadOnlyException>(() => DataAccess.ProductsManager.Remove(product.Id));
            Assert.NotNull(DataAccess.ProductsManager.GetById(1));
        }
    }
}