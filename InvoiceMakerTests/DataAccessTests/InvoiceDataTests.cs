using System;
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
                Assert.AreEqual($"{DateTime.Today.Year}/{i+1}", DataAccess.InvoiceManager.GetById(i+1).Number);
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

        [Test]
        public void RemoveInvoiceTest()
        {
            var products = DataObjectsMock.MockProducts(3, DataAccess.ProductsManager);
            var invoice = DataObjectsMock.MockInvoice(DataObjectsMock.MockClient(0), 0);
            invoice.Products.Add(DataObjectsMock.MockInvoiceEntry(products[0], 1));
            invoice.Products.Add(DataObjectsMock.MockInvoiceEntry(products[1], 1));
            invoice.Products.Add(DataObjectsMock.MockInvoiceEntry(products[2], 1));
            
            DataAccess.InvoiceManager.Add(invoice);
            Assert.NotNull(DataAccess.InvoiceManager.GetById(1));
            DataAccess.InvoiceManager.Remove(1);
            Assert.Null(DataAccess.InvoiceManager.GetById(1));
            Assert.AreEqual(3, DataAccess.ProductsManager.GetAll().Count());
            Assert.AreEqual(1, DataAccess.CurrencyManager.GetAll().Count());
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

        [Test]
        public void IsInvoiceAfterDeadlineTest()
        {
            var client = DataObjectsMock.MockClient(0);
            var invoice_0 = DataObjectsMock.MockInvoice(client, 0);
            var invoice_1 = DataObjectsMock.MockInvoice(client, 1);
            invoice_1.PaymentDate = new DateTime();
            invoice_1.PaymentTerm = DateTime.Today.AddDays(-5);

            Assert.False(DataAccess.InvoiceManager.IsAfterDeadline(invoice_0));
            Assert.True(DataAccess.InvoiceManager.IsAfterDeadline(invoice_1));
        }

        [Test]
        public void GetAllInvoicesAfterDeadlineTest()
        {
            var client = DataObjectsMock.MockClient(0);
            var invoice_0 = DataObjectsMock.MockInvoice(client, 0);
            invoice_0.PaymentDate = new DateTime();
            invoice_0.PaymentTerm = DateTime.Today;
            var invoice_1 = DataObjectsMock.MockInvoice(client, 1);
            invoice_1.PaymentDate = new DateTime();
            invoice_1.PaymentTerm = DateTime.Today.AddDays(-1);
            var invoice_2 = DataObjectsMock.MockInvoice(client, 2);
            invoice_2.PaymentDate = new DateTime();
            invoice_2.PaymentTerm = DateTime.Today.AddDays(-5);
            var invoice_3 = DataObjectsMock.MockInvoice(client, 3);
            var invoice_4 = DataObjectsMock.MockInvoice(client, 4);
            
            DataAccess.InvoiceManager.Add(invoice_0);
            DataAccess.InvoiceManager.Add(invoice_1);
            DataAccess.InvoiceManager.Add(invoice_2);
            DataAccess.InvoiceManager.Add(invoice_3);
            DataAccess.InvoiceManager.Add(invoice_4);
            
            Assert.AreEqual(5, DataAccess.InvoiceManager.GetAll().Count());
            Assert.AreEqual(2, DataAccess.InvoiceManager.GetAllAfterDeadline().Count());
        }

        [TestCase(3)]
        [TestCase(10)]
        [TestCase(30)]
        public void GetAllInvoicesWithDeadlineInDaysTest(int days)
        {
            var client = DataObjectsMock.MockClient(0);
            var invoice_0 = DataObjectsMock.MockInvoice(client, 0);
            invoice_0.PaymentDate = new DateTime();
            invoice_0.PaymentTerm = DateTime.Today;
            var invoice_1 = DataObjectsMock.MockInvoice(client, 1);
            invoice_1.PaymentDate = new DateTime();
            invoice_1.PaymentTerm = DateTime.Today.AddDays(3);
            var invoice_2 = DataObjectsMock.MockInvoice(client, 2);
            invoice_2.PaymentDate = new DateTime();
            invoice_2.PaymentTerm = DateTime.Today.AddDays(10);
            var invoice_3 = DataObjectsMock.MockInvoice(client, 3);
            invoice_3.PaymentDate = new DateTime();
            invoice_3.PaymentTerm = DateTime.Today.AddDays(25);
            var invoice_4 = DataObjectsMock.MockInvoice(client, 4);
            
            DataAccess.InvoiceManager.Add(invoice_0);
            DataAccess.InvoiceManager.Add(invoice_1);
            DataAccess.InvoiceManager.Add(invoice_2);
            DataAccess.InvoiceManager.Add(invoice_3);
            DataAccess.InvoiceManager.Add(invoice_4);
            
            Assert.AreEqual(5, DataAccess.InvoiceManager.GetAll().Count());
            var expected = days switch
            {
                3 => 1,
                10 => 2,
                30 => 4
            };

            Assert.AreEqual(expected, DataAccess.InvoiceManager.GetAllWithTerm(days).Count());
        }
    }
}