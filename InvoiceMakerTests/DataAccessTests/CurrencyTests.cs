using System.Data;
using System.Linq;
using InvoiceMakerTests.MockHelpers;
using NUnit.Framework;

namespace InvoiceMakerTests.DataAccessTests
{
    public class CurrencyTests : DataAccessMockSetup
    {
        [Test]
        public void CreateCurrencyTest()
        {
            var currency = DataObjectsMock.MockCurrency(0);
            DataAccess.CurrencyManager.Add(currency);
            
            Assert.NotNull(DataAccess.CurrencyManager.GetById(1));
        }

        [Test]
        public void RemoveCurrencyTest()
        {
            var currency_0 = DataObjectsMock.MockCurrency(0);
            var currency_1 = DataObjectsMock.MockCurrency(1);
            var invoice = DataObjectsMock.MockInvoice(DataObjectsMock.MockClient(0), 0);
            invoice.CurrencyModel = currency_1;
            
            DataAccess.CurrencyManager.Add(currency_0);
            DataAccess.InvoiceManager.Add(invoice);
            
            Assert.AreEqual(2, DataAccess.CurrencyManager.GetAll().Count());
            Assert.AreEqual(currency_1, DataAccess.InvoiceManager.GetById(1).CurrencyModel);
            
            DataAccess.CurrencyManager.Remove(1);
            DataAccess.CurrencyManager.Remove(2);
            Assert.IsNull(DataAccess.CurrencyManager.GetById(1));
            Assert.AreEqual("NO_CURRENCY", invoice.CurrencyModel.Name);
        }

        [Test]
        public void UpdateCurrencyTest()
        {
            var currency = DataObjectsMock.MockCurrency(0);
            var invoice = DataObjectsMock.MockInvoice(DataObjectsMock.MockClient(0), 0);
            invoice.CurrencyModel = currency;
            DataAccess.InvoiceManager.Add(invoice);
            
            Assert.AreEqual("Currency_0", invoice.CurrencyModel.Name);
            currency.Name = "Updated Currency";
            DataAccess.SaveChanges();
            Assert.AreEqual("Updated Currency", invoice.CurrencyModel.Name);
        }
    }
}