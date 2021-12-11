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
                DataAccess.ProductsManager.AddProduct(newProduct);
                Assert.NotNull(DataAccess.ProductsManager.GetProductByName(newProduct.Name));
            }
            
            Assert.AreEqual(number, DataAccess.ProductsManager.GetAllProducts().Count());
        }

        [Test]
        public void UpdateProductTest()
        {
            var product = DataObjectsMock.MockProduct(1);
            DataAccess.ProductsManager.AddProduct(product);
            
            product.Name = "Updated";
            product.DefaultPrice = 2;
            DataAccess.ProductsManager.UpdateProduct(1, product);
            var updatedProduct = DataAccess.ProductsManager.GetProductById(1);
            Assert.AreEqual(product, updatedProduct);
        }

        [Test]
        public void RemoveProductTest()
        {
            DataAccess.ProductsManager.AddProduct(DataObjectsMock.MockProduct(0));
            DataAccess.ProductsManager.AddProduct(DataObjectsMock.MockProduct(1));
            DataAccess.ProductsManager.AddProduct(DataObjectsMock.MockProduct(2));
            DataAccess.ProductsManager.AddProduct(DataObjectsMock.MockProduct(3));
            
            Assert.AreEqual(4, DataAccess.ProductsManager.GetAllProducts().Count());
            DataAccess.ProductsManager.RemoveProduct(2);
            Assert.AreEqual(3, DataAccess.ProductsManager.GetAllProducts().Count());
            Assert.IsEmpty(DataAccess.ClientsManager.GetByName("Product_1"));
        }
    }
}