using System;
using System.Collections.Generic;
using System.Linq;
using InvoiceMakerCore.Models;
using NUnit.Framework;

namespace InvoiceMakerTests.SqlLite
{
    public class SqlLiteCreateObjectsTests : SqlLiteMockupSetup
    {
        [TestCase(1)]
        [TestCase(5)]
        public void CreateSqlLiteClientsTest(int number)
        {
            for (var i = 0; i < number; i++)
            {
                DataBaseAccess.Clients.Add(new ClientModel { Name = $"Client_{i}" });
                DataBaseAccess.SaveChanges();
                Assert.NotNull(DataBaseAccess.Clients.FirstOrDefault(x=> x.Name == $"Client_{i}"));    
            }
            
            Assert.AreEqual(number, DataBaseAccess.Clients.Count());
        }
        
        [TestCase(1)]
        [TestCase(5)]
        public void CreateSqlLiteProductsTest(int number)
        {
            for (var i = 0; i < number; i++)
            {
                DataBaseAccess.Products.Add(new ProductModel()
                {
                    Name = $"Product_{i}",
                    DefaultPrice = i
                });
                DataBaseAccess.SaveChanges();
                var product = DataBaseAccess.Products.FirstOrDefault(x => x.Name == $"Product_{i}");
                Assert.NotNull(product);
                Assert.AreEqual(i, product.DefaultPrice);
            }
            
            Assert.AreEqual(number, DataBaseAccess.Products.Count());
        }
        
        [TestCase(1)]
        [TestCase(5)]
        public void CreateSqlLiteCurrencyTest(int number)
        {
            for (var i = 0; i < number; i++)
            {
                DataBaseAccess.Currencies.Add(new CurrencyModel($"Currency_{i}"));
                DataBaseAccess.SaveChanges();
                var product = DataBaseAccess.Currencies.FirstOrDefault(x => x.Name == $"Currency_{i}");
                Assert.NotNull(product);
            }
            
            Assert.AreEqual(number, DataBaseAccess.Currencies.Count());
        }
        
        [TestCase(1)]
        [TestCase(5)]
        public void CreateSqlLiteInvoiceTest(int number)
        {
            for (var i = 0; i < number; i++)
            {
                DataBaseAccess.Invoices.Add(new InvoiceModel()
                {
                    Number = $"Invoice_{i}",
                    CreationDate = DateTime.Today.AddDays(i),
                    PaymentTerm = DateTime.Today.AddDays(i+1),
                    PaymentDate = DateTime.Today.AddDays(i+2),
                    CurrencyModel = new CurrencyModel("TestCurrency"),
                    Client = new ClientModel(),
                    Products = { new InvoiceProductEntryModel() }
                });
                DataBaseAccess.SaveChanges();
                var product = DataBaseAccess.Invoices.FirstOrDefault(x => x.Number == $"Invoice_{i}");
                Assert.NotNull(product);
            }
            
            Assert.AreEqual(number, DataBaseAccess.Invoices.Count());
        }
    }
}