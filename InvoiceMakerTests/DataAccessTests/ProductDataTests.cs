using System.Linq;
using InvoiceMakerCore.Annotations.Builders;
using InvoiceMakerCore.Models;
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
                var newProduct = MockProduct(i);
                DataAccess.ProductsManager.AddProduct(newProduct);
                Assert.NotNull(DataAccess.ProductsManager.GetProductByName(newProduct.Name));
            }
            
            Assert.AreEqual(number, DataAccess.ProductsManager.GetAllProducts().Count());
        }

        [Test]
        public void UpdateProductTest()
        {
            var product = MockProduct(1);
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
            DataAccess.ProductsManager.AddProduct(MockProduct(0));
            DataAccess.ProductsManager.AddProduct(MockProduct(1));
            DataAccess.ProductsManager.AddProduct(MockProduct(2));
            DataAccess.ProductsManager.AddProduct(MockProduct(3));
            
            Assert.AreEqual(4, DataAccess.ProductsManager.GetAllProducts().Count());
            DataAccess.ProductsManager.RemoveProduct(2);
            Assert.AreEqual(3, DataAccess.ProductsManager.GetAllProducts().Count());
            Assert.IsEmpty(DataAccess.ClientsManager.GetClientsByName("Product_1"));
        }

        private ProductModel MockProduct(int number)
        {
            return new ProductBuilder()
                .AddName($"Product_{number}")
                .AddDefaultPrice(number)
                .AddUnit(new UnitModel() { Name = "TestUnit"})
                .Build();
        }
    }
}