using System;
using System.Collections.Generic;
using InvoiceMakerCore.Annotations.Builders;
using InvoiceMakerCore.Managers.DataManagement;
using InvoiceMakerCore.Managers.DataManagement.HighLevelDataManagers;
using InvoiceMakerCore.Models;

namespace InvoiceMakerTests.MockHelpers
{
    public static class DataObjectsMock
    {
        public static ProductModel MockProduct(int number)
        {
            return new ProductBuilder()
                .AddName($"Product_{number}")
                .AddDefaultPrice(number)
                .AddUnit(MockUnit(0))
                .Build();
        }
        
        public static List<ProductModel> MockProducts(int productsNumber, ProductManager manager)
        {
            var products = new List<ProductModel>();

            for (var i = 0; i < productsNumber; i++)
            {
                var product = MockProduct(i);
                manager.Add(product);
                products.Add(product);
            }

            return products;
        }

        public static UnitModel MockUnit(int number)
        {
            return new UnitBuilder()
                .AddName($"Unit_{number}")
                .Build();
        }
        
        public static ClientModel MockClient(int number)
        {
            return new ClientBuilder()
                .AddName($"Client_{number}")
                .Build();
        }

        public static InvoiceModel MockInvoice(ClientModel client, int number)
        {
            return new InvoiceBuilder(client)
                .AddCreationDate(DateTime.Today)
                .AddPaymentTerm(DateTime.Today.AddDays(5))
                .AddPaymentDate(DateTime.Today.AddDays(4))
                .Build();
        }

        public static InvoiceProductEntryModel MockInvoiceEntry(ProductModel product, int number)
        {
            return new InvoiceEntryBuilder(product)
                .AddQuantity(number)
                .AddPricePreUnit(number)
                .Build();
        }

        public static CurrencyModel MockCurrency(int i)
        {
            return new CurrencyBuilder()
                .AddName($"Currency_{i}")
                .Build();
        }
    }
}